using ElasticsearchApi.Model;

namespace ElasticsearchApi.Services
{
    public interface IElasticsearchService
    {
        public interface IElasticsearchService<T> where T : class
        {
            Task IndexDocumentAsync(T item);
            Task<T> GetDocumentByIdAsync(string id);
            Task UpdateDocumentAsync(string id, T item);
            Task DeleteDocumentAsync(string id);
            Task<IEnumerable<T>> SearchDocumentsAsync(string searchText);
        }
    }
}
