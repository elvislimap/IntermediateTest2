using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Application.Services;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Interfaces.Services;
using IntermediateTest2.Domain.Services;
using IntermediateTest2.Infra.Data.Repositories;
using IntermediateTest2.Infra.Security.Interfaces;
using IntermediateTest2.Infra.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IntermediateTest2.Infra.CrossCutting.Ioc
{
    public static class ConfigStrapper
    {
        public static void RegisterServicesIoc(this IServiceCollection services)
        {
            #region AppServices

            services.AddScoped<IEmployeeAppService, EmployeeAppService>();
            services.AddScoped<IInflationAdjustAppService, InflationAdjustAppService>();
            services.AddScoped<ISharedFundAppService, SharedFundAppService>();

            #endregion

            #region Services

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IInflationAdjustService, InflationAdjustService>();
            services.AddScoped<IJwtAuthService, JwtAuthService>();

            #endregion

            #region Repositories

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IInflationAdjustRepository, InflationAdjustRepository>();
            services.AddScoped<ISharedFundRepository, SharedFundRepository>();

            #endregion
        }
    }
}