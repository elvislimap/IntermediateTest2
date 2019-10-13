using IntermediateTest2.Infra.CrossCutting.Ioc;
using IntermediateTest2.Service.Api.Commons;
using IntermediateTest2.Service.Api.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntermediateTest2.Service.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServicesSwagger();
            services.RegisterServicesApi(Configuration);
            services.RegisterServicesIoc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.RegisterApplicationSwagger();
            app.UseExceptionHandler(config => config.DefaultExceptionHandler());
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvc();
        }
    }
}
