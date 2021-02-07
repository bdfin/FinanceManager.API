using FinanceManager.Models;
using FinanceManager.Models.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace FinanceManager.Services
{
    public class CosmosService : ICosmosService
    {
        private readonly CosmosCredentials credentials;
        private readonly CosmosClient cosmosClient;
        private readonly Container container;

        public CosmosService(CosmosCredentials credentials)
        {
            this.credentials = credentials;

            var options = new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                    IgnoreNullValues = true
                }
            };

            cosmosClient = new CosmosClient(credentials.Endpoint, credentials.Key, options);
            container = cosmosClient.GetContainer(credentials.Database, credentials.Collection);
        }

        public async Task<T> CreateItemAsync<T>(T item)
        {
            if (item is IBaseModel)
            {
                (item as IBaseModel).CreatedAt = DateTimeOffset.UtcNow;
                (item as IBaseModel).UpdatedAt = DateTimeOffset.UtcNow;
            }

            var result = await container.CreateItemAsync(item, new PartitionKey(credentials.PartitionKey));

            return result.StatusCode is HttpStatusCode.Created ? result.Resource : default;
        }

        public async Task<T> UpdateItemAsync<T>(string id, T item)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            if (item is IBaseModel)
                (item as IBaseModel).UpdatedAt = DateTimeOffset.UtcNow;

            var result = await container.ReplaceItemAsync(item, id, new PartitionKey(credentials.PartitionKey));

            return result.StatusCode is HttpStatusCode.OK ? result.Resource : default;
        }

        public async Task<bool> DeleteItemAsync<T>(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var result = await container.DeleteItemAsync<T>(id, new PartitionKey(credentials.PartitionKey));

            return result.StatusCode is HttpStatusCode.OK;
        }

        public async Task<T> LoadItem<T>(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var result = await container.ReadItemAsync<T>(id, new PartitionKey(credentials.PartitionKey));

            return result.StatusCode is HttpStatusCode.OK ? result.Resource : default;
        }

        public IEnumerable<T> LoadItems<T>(Expression<Func<T, bool>> predicate)
        {
            List<T> results = null;

            var iterator = container.GetItemLinqQueryable<T>(true).Where(predicate).ToFeedIterator();

            if (iterator.HasMoreResults)
            {
                results = new List<T>();
                while (iterator.HasMoreResults)
                {
                    results.AddRange(iterator.ReadNextAsync().Result.Resource);
                }
            }

            return results;
        }

        public IEnumerable<T> LoadAllItems<T>()
        {
            var iterator = container.GetItemLinqQueryable<T>().ToFeedIterator();

            var results = iterator.ReadNextAsync().Result.Resource;

            return results;
        }
    }
}
