using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Service.Api.Commons;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntermediateTest2.Service.Api.Controllers
{
    [Route("api/employee/v1")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeAppService _employeeAppService;

        public EmployeeController(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }


        [HttpGet("GetAll")]
        public Task<ObjectResult> GetAll()
        {
            return _employeeAppService.GetAll().TaskResult();
        }

        [HttpPost("Add")]
        public Task<ObjectResult> Add([FromBody]Employee employee)
        {
            return _employeeAppService.Add(employee).TaskResult();
        }
    }
}