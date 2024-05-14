using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ElasticsearchApi.Services.IElasticsearchService;

namespace ElasticsearchApi.Core
{
    [Route("api/[controller]")]
    //[ApiController]
    public class GenericBaseController<T> : ControllerBase where T : class
    {
        private readonly IElasticsearchService<T> _elasticService;

        public GenericBaseController(IElasticsearchService<T> elasticService)
        {
            _elasticService = elasticService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T item)
        {
            await _elasticService.IndexDocumentAsync(item);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _elasticService.GetDocumentByIdAsync(id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] T item)
        {
            await _elasticService.UpdateDocumentAsync(id, item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _elasticService.DeleteDocumentAsync(id);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            var results = await _elasticService.SearchDocumentsAsync(searchText);
            return Ok(results);
        }
    }
}
