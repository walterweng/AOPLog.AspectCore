using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPLogger.AspectCore.Domain;
using AOPLogger.AspectCore.LogAttribute;
using AOPLogger.AspectCore.Service;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;

namespace AOPLogger.AspectCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<ICustomService, CustomService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddSingleton<CheckLogMessage, CheckLogMessage>();
            services.AddSingleton<AccountLogMessage, AccountLogMessage>();

            services.AddSingleton<INLogWriteService, NLogWriteService>();

            services.ConfigureDynamicProxy(config => { config.Interceptors.AddServiced<CheckLogMessage>(Predicates.ForMethod("*Check*")); });
            services.ConfigureDynamicProxy(config => { config.Interceptors.AddServiced<AccountLogMessage>(Predicates.ForMethod("GetId")); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
