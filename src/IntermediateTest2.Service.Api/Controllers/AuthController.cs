using IntermediateTest2.Domain.Commons;
using IntermediateTest2.Domain.ValueObjects;
using IntermediateTest2.Infra.Security.Interfaces;
using IntermediateTest2.Service.Api.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntermediateTest2.Service.Api.Controllers
{
    [Route("api/auth/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IJwtAuthService _jwtAuthService;

        public AuthController(TokenConfigurations tokenConfigurations, IJwtAuthService jwtAuthService)
        {
            _tokenConfigurations = tokenConfigurations;
            _jwtAuthService = jwtAuthService;
        }

        [AllowAnonymous]
        [HttpPost("Token")]
        public Task<ObjectResult> Token([FromBody] RequestToken requestToken)
        {
            if (requestToken.User != "Itermediate2" || requestToken.Password != "Ct2JZAFqA59qL9G3")
                return new ResponseToken(false, "User or password is invalid").TaskResult();

            var uniqueName = requestToken.User;
            var createdDate = DateTime.Now;
            var expiressDate = createdDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);
            var token = _jwtAuthService.GenerateToken(uniqueName, createdDate, expiressDate);

            return new ResponseToken(true, null, createdDate.ToDateTimeEn(),
                expiressDate.ToDateTimeEn(), token).TaskResult();
        }
    }
}