using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace lovepdf
{
    public class Startup
    {
        // 构造函数
        // 注入配置对象
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;  
        }

        public IConfiguration Configuration { get; }

        // 配置系统所使用的各种服务
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 使用扩展方法配置服务
            services.ConfigureCore();

            // 为控制器配置服务
            // 默认情况下，ASP.NET Core 对于控制器并不是从容器中创建的
            // 如果需要将控制器本身也作为服务注册，需要调用 AddControllersAsServices()
            services.AddControllers()
                .AddControllersAsServices();

            // 支持版本化
            services.AddApiVersioning( options => {
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            // 配置 Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "lovepdf", Version = "v1" });
            });
        }

        // Writing logs before completion of the DI container setup in the Startup.ConfigureServices method is not supported:
        // Logger injection into the Startup constructor is not supported.
        // Logger injection into the Startup.ConfigureServices method signature is not supported

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation( $"MyConfig: {Configuration["MyConfig"]}" );

            // 开发模式
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "lovepdf v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // 使用特性路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
