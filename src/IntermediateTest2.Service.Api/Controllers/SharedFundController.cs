using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Service.Api.Commons;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntermediateTest2.Service.Api.Controllers
{
    [Route("api/sharedfund/v1")]
    public class SharedFundController : ControllerBase
    {
        private readonly ISharedFundAppService _sharedFundAppService;

        public SharedFundController(ISharedFundAppService sharedFundAppService)
        {
            _sharedFundAppService = sharedFundAppService;
        }


        [HttpGet("GetBalance/{employeeId}")]
        public Task<ObjectResult> GetBalance(int employeeId)
        {
            return _sharedFundAppService.GetBalance(employeeId).TaskResult();
        }

        [HttpPost("Add")]
        public Task<ObjectResult> Add([FromBody]SharedFund sharedFund)
        {
            return _sharedFundAppService.Add(sharedFund).TaskResult();
        }

        [HttpPost("Withdraw")]
        public Task<ObjectResult> Withdraw([FromBody]int employeeId)
        {
            return _sharedFundAppService.Withdraw(employeeId).TaskResult();
        }
    }
}