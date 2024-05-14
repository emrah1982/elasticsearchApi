using ElasticsearchApi.Core;
using ElasticsearchApi.Model;
using ElasticsearchApi.Services;
using Microsoft.AspNetCore.Mvc;
using static ElasticsearchApi.Services.IElasticsearchService;

namespace ElasticsearchApi.Controllers
{
    [Route("api/companies")]
    public class CompanysController : GenericBaseController<CompanyDTO>
    {
        public CompanysController(IElasticsearchService<CompanyDTO> service) : base(service) { }
    }

}
