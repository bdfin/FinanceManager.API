using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FinanceManager.Services
{
    public interface ICosmosService
    {
        Task<T> CreateItemAsync<T>(T item);
        Task<bool> DeleteItemAsync<T>(string id);
        IEnumerable<T> LoadAllItems<T>();
        Task<T> LoadItem<T>(string id);
        IEnumerable<T> LoadItems<T>(Expression<Func<T, bool>> predicate);
        Task<T> UpdateItemAsync<T>(string id, T item);
    }
}