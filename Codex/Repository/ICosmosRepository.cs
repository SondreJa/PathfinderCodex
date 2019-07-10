using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICosmosRepository<T>
    {
        Task Upsert(DocumentWrapper<T> entity);
        Task<DocumentWrapper<T>> Get(string id);
        Task<IEnumerable<DocumentWrapper<T>>> GetMany(Expression<Func<T, bool>> predicate);
        Task Delete(string id);
    }
}
