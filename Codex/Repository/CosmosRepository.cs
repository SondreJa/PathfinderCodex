using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Repository
{
    public class CosmosRepository<T> : ICosmosRepository<T> where T : class, new()
    {
        private readonly DocumentClient client;
        private readonly Uri collectionUri;
        private readonly string databaseId;
        private readonly string collectionId;

        public CosmosRepository(Uri serviceEndpoint, string authToken, string databaseId, string collectionId, string partitionKey = "/id")
        {
            this.databaseId = databaseId;
            this.collectionId = collectionId;

            client = new DocumentClient(serviceEndpoint, authToken);
            collectionUri = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

            // Setup if not exists
            var database = new Database { Id = databaseId };
            client.CreateDatabaseIfNotExistsAsync(database).GetAwaiter().GetResult();

            var databaseUri = UriFactory.CreateDatabaseUri(databaseId);
            var collection = new DocumentCollection { Id = collectionId };
            collection.PartitionKey.Paths.Add(partitionKey);
            client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, collection).GetAwaiter().GetResult();
        }

        public async Task Delete(string id)
        {
            var requestOptions = new RequestOptions { PartitionKey = new PartitionKey(id) };
            var uri = UriFactory.CreateDocumentUri(databaseId, collectionId, id);
            try
            {
                await client.DeleteDocumentAsync(uri, requestOptions);
            }
            catch(DocumentClientException e) when (e.StatusCode == HttpStatusCode.NotFound) { return; }
        }

        public async Task<DocumentWrapper<T>> Get(string id)
        {
            var requestOptions = new RequestOptions { PartitionKey = new PartitionKey(id) };
            var uri = UriFactory.CreateDocumentUri(databaseId, collectionId, id);
            try
            {
                var response = await client.ReadDocumentAsync<dynamic>(uri, requestOptions);
                return response.Document;
            }
            catch (DocumentClientException e) when (e.StatusCode == HttpStatusCode.NotFound) { return null; }
        }

        public async Task<IEnumerable<DocumentWrapper<T>>> GetMany(Expression<Func<T, bool>> predicate)
        {
            var feedOptions = new FeedOptions { EnableCrossPartitionQuery = true };
            var query = client.CreateDocumentQuery<T>(collectionUri, feedOptions)
                .Where(predicate)
                .AsDocumentQuery();

            var results = new List<DocumentWrapper<T>>();

            while (query.HasMoreResults)
            {
                FeedResponse<dynamic> documents = await query.ExecuteNextAsync();
                var result = documents.Select(document => new DocumentWrapper<T>((Document)document));
                results.AddRange(result);
            }

            return results;
        }

        public async Task Upsert(DocumentWrapper<T> entity)
        {
            await client.UpsertDocumentAsync(collectionUri, entity);
        }
    }
}
