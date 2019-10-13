using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Service.Api.Commons;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntermediateTest2.Service.Api.Controllers
{
    [Route("api/inflationadjust/v1")]
    public class InflationAdjustController : ControllerBase
    {
        private readonly IInflationAdjustAppService _inflationAdjustAppService;

        public InflationAdjustController(IInflationAdjustAppService inflationAdjustAppService)
        {
            _inflationAdjustAppService = inflationAdjustAppService;
        }


        [HttpGet("GetAll")]
        public Task<ObjectResult> GetAll()
        {
            return _inflationAdjustAppService.GetAll().TaskResult();
        }

        [HttpPost("Add")]
        public Task<ObjectResult> Add([FromBody]InflationAdjust inflationAdjust)
        {
            return _inflationAdjustAppService.Add(inflationAdjust).TaskResult();
        }
    }
}