using ElasticsearchApi.Core;
using ElasticsearchApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ElasticsearchApi.Services.IElasticsearchService;

namespace ElasticsearchApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Route("api/employees")]
    public class EmployeeController : GenericBaseController<EmployeeDTO>
    {
        public EmployeeController(IElasticsearchService<EmployeeDTO> service) : base(service) { }
    }
}
