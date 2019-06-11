using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Demo.Monolith.API.Data.Contracts.v1;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace Demo.Monolith.API.Data.Providers
{
    public class TableStorageAccessor
    {
        private readonly CloudTableClient _tableClient;

        public TableStorageAccessor(IConfiguration configuration)
        {
            _tableClient = CreateTableClient(configuration);
        }

        public async Task PersistAsync<TEntity>(string tableName, TEntity entity)
            where TEntity : TableEntity
        {
            var table = await GetTableAsync(tableName);

            var insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(insertOrMergeOperation);
        }

        public async Task<TEntity> GetAsync<TEntity>(string tableName, string partitionKey, string rowKey)
            where TEntity : TableEntity
        {
            var table = await GetTableAsync(tableName);

            var retrieve = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
            var tableOperationResult = await table.ExecuteAsync(retrieve);

            switch (tableOperationResult.HttpStatusCode)
            {
                case (int)HttpStatusCode.OK:
                    return (TEntity)tableOperationResult.Result;
                case (int)HttpStatusCode.NotFound:
                    return null;
                default:
                    throw new Exception($"Failed to look up table entity with partition key '{partitionKey}' and row key '{rowKey}' from table '{tableName}'");
            }
        }

        public async Task<List<TEntity>> GetAsync<TEntity>(string tableName, string partitionKey)
            where TEntity : TableEntity, new()
        {
            var table = await GetTableAsync(tableName);

            var query = new TableQuery<TEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            var foundItems = table.ExecuteQuery(query);
            return foundItems.ToList();
        }

        private async Task<CloudTable> GetTableAsync(string tableName)
        {
            CloudTable table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }

        private CloudTableClient CreateTableClient(IConfiguration configuration)
        {
            var tableConnectionString = configuration["TableConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(tableConnectionString);
            return storageAccount.CreateCloudTableClient();
        }
    }
}
