using ElasticsearchApi.Services;
using ElasticsearchApi.Model;
using Nest;
using static ElasticsearchApi.Services.IElasticsearchService;

namespace ElasticsearchApi.Services
{
   
        public class ElasticsearchService<T> : IElasticsearchService<T> where T : class
        {
        private readonly IElasticClient _elasticClient;
        private readonly string _indexName;

        public ElasticsearchService(IElasticClient elasticClient, string indexName)
        {
            _elasticClient = elasticClient;
            _indexName = indexName;
        }

        public async Task IndexDocumentAsync(T item)
        {
            var response = await _elasticClient.IndexDocumentAsync(item);
            if (!response.IsValid)
            {
                throw new Exception($"Failed to index document: {response.DebugInformation}");
            }
        }

        public async Task<T> GetDocumentByIdAsync(string id)
        {
            var response = await _elasticClient.GetAsync<T>(id, idx => idx.Index(_indexName));
            if (!response.IsValid)
            {
                throw new Exception($"Failed to retrieve document: {response.DebugInformation}");
            }

            return response.Source;
        }

        public async Task UpdateDocumentAsync(string id, T item)
        {
            var response = await _elasticClient.UpdateAsync<T>(id, u => u.Doc(item).Index(_indexName));
            if (!response.IsValid)
            {
                throw new Exception($"Failed to update document: {response.DebugInformation}");
            }
        }

        public async Task DeleteDocumentAsync(string id)
        {
            var response = await _elasticClient.DeleteAsync<T>(id, d => d.Index(_indexName));
            if (!response.IsValid)
            {
                throw new Exception($"Failed to delete document: {response.DebugInformation}");
            }
        }

        public async Task<IEnumerable<T>> SearchDocumentsAsync(string searchText)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s
                .Index(_indexName)
                .Query(q => q
                    .QueryString(qs => qs
                        .Query(searchText)
                    )
                )
            );

            if (!response.IsValid)
            {
                throw new Exception($"Failed to search documents: {response.DebugInformation}");
            }

            return response.Documents;
        }

    }       
    
}
