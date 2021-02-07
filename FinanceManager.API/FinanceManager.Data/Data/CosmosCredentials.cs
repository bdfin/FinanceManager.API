using System;

namespace FinanceManager.Models.Data
{
    public class CosmosCredentials
    {
        public CosmosCredentials(string endpoint,
            string accountKey,
            string database,
            string collection,
            string partitionKey = "")
        {
            Endpoint = endpoint;
            Key = accountKey;
            Database = database;
            Collection = collection;
            PartitionKey = partitionKey;

            Validate();
        }

        public string Endpoint { get; set; }
        public string Key { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
        public string PartitionKey { get; set; }

        public void Validate()
        {
            if (this is null)
                throw new ArgumentNullException(nameof(CosmosCredentials));

            if (string.IsNullOrWhiteSpace(Key))
                throw new ArgumentNullException(nameof(Key));

            if (string.IsNullOrWhiteSpace(Collection))
                throw new ArgumentNullException(nameof(Collection));

            if (string.IsNullOrWhiteSpace(Database))
                throw new ArgumentNullException(nameof(Database));

            if (string.IsNullOrWhiteSpace(Endpoint))
                throw new ArgumentNullException(nameof(Endpoint));
        }
    }
}
