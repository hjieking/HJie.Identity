using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HJie.Identity.ApiService01
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
            // IdentityServer
            services.AddMvcCore().AddAuthorization().AddJsonFormatters();
            services.AddAuthentication(Configuration["Identity:Scheme"])
                .AddIdentityServerAuthentication(options =>
                {
                    //获取或设置元数据地址或权限是否需要HTTPS。默认值为true。这应该只在开发环境中禁用。
                    options.RequireHttpsMetadata = false; // for dev env
                    //授权服务器的IP地址
                    options.Authority = $"http://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";
                    //授权服务器配置的API资源的名称
                    options.ApiName = Configuration["Service:Name"]; // match with configuration in IdentityServer
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // authentication 身份验证  认证
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
