using IntermediateTest2.Domain.ValueObjects;
using IntermediateTest2.Infra.Data.Context;
using IntermediateTest2.Infra.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IntermediateTest2.Service.Api.Commons
{
    public static class Extensions
    {
        public static void RegisterServicesApi(this IServiceCollection services, IConfiguration configuration)
        {
            var authorizationPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();

            JwtAuthService.RegisterLogin(services, configuration, authorizationPolicy);

            services.AddCors();

            services.AddDbContext<ContextEf>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("IntermediateContext"),
                b => b.MigrationsAssembly("IntermediateTest2.Service.Api")));

            services.AddMvc(opt => opt.Filters.Add(new AuthorizeFilter(authorizationPolicy)))
                .AddJsonOptions(opt => opt.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public static void RegisterServicesSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v1", new Info
                {
                    Title = "IntermediateTest2 - HTTP API",
                    Version = "v1"
                });
            });
        }

        public static void RegisterApplicationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }

        public static Task<ObjectResult> TaskResult(this ResponseToken responseToken)
        {
            var taskResult = new TaskCompletionSource<ObjectResult>();
            var objectResult = new ObjectResult(responseToken) { StatusCode = responseToken.GetStatusCode() };

            taskResult.SetResult(objectResult);

            return taskResult.Task;
        }

        public static Task<ObjectResult> TaskResult(this Result result)
        {
            var taskResult = new TaskCompletionSource<ObjectResult>();
            var objectResult = new ObjectResult(result) { StatusCode = result.GetStatusCode() };

            taskResult.SetResult(objectResult);

            return taskResult.Task;
        }


        private static int GetStatusCode(this ResponseToken responseToken)
        {
            return !responseToken.Authenticated ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;
        }

        private static int GetStatusCode(this Result result)
        {
            return result.ValidationErrors.Any() || result.MessageErrors.Any()
                ? (int)HttpStatusCode.BadRequest
                : (int)HttpStatusCode.OK;
        }
    }
}