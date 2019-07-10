using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace Repository.Models
{
    public class DocumentWrapper<T>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public T Entity { get; set; }

        public DocumentWrapper()
        {

        }

        public DocumentWrapper(Document document) : base()
        {
            var json = JsonConvert.SerializeObject(document);
            var jsonDoc = JsonConvert.DeserializeObject<DocumentWrapper<T>>(json);

            Id = document.Id;
            Timestamp = document.Timestamp;
            Entity = jsonDoc.Entity;
        }
        
        public static implicit operator DocumentWrapper<T>(Document document)
        {
            if (document == null)
                return null;
            return new DocumentWrapper<T>(document);
        }
    }
}
