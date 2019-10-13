using IntermediateTest2.Domain.Commons;
using IntermediateTest2.Domain.ValueObjects;
using IntermediateTest2.Infra.Security.Interfaces;
using IntermediateTest2.Service.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntermediateTest2.Service.Api.Tests.Controllers
{
    public class AuthControllerTest
    {
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly Mock<IJwtAuthService> _jwtAuthService;
        private AuthController _authController;

        public AuthControllerTest()
        {
            _tokenConfigurations = new TokenConfigurations { Seconds = 60 };
            _jwtAuthService = new Mock<IJwtAuthService>();
        }


        [Fact]
        public async Task AuthController_TokenOk()
        {
            var token = Guid.NewGuid().ToText();
            var objectResult = await
                GetToken(token, new RequestToken { User = "Itermediate2", Password = "Ct2JZAFqA59qL9G3" });
            var responseToken = ((ResponseToken)objectResult.Value);

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(token, responseToken.AccessToken);
            Assert.True(responseToken.Authenticated);
        }

        [Fact]
        public async Task AuthController_TokenError()
        {
            var objectResult = await
                GetToken(Guid.NewGuid().ToText(), new RequestToken { User = "Itermediate2", Password = "abcd" });
            var responseToken = ((ResponseToken)objectResult.Value);

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
            Assert.Null(responseToken.AccessToken);
            Assert.False(responseToken.Authenticated);
        }


        private async Task<ObjectResult> GetToken(string token, RequestToken requestToken)
        {
            _jwtAuthService
                .Setup(j => j.GenerateToken(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(token);

            _authController = new AuthController(_tokenConfigurations, _jwtAuthService.Object);

            return await _authController.Token(requestToken);
        }
    }
}