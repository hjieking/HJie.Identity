using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace HJie.Identity.SwaggerGen
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "aaaaaaaa",
                    Version = "v1",
                    Description = "",
                    TermsOfService = "http://hui.dgjy.net",
                    Contact = new Contact
                    {
                        Name = "谢海棠",
                        Email = "xieht@alltosea.com",
                        Url = "http://www.alltosea.com"
                    }
                });

                //options.OperationFilter<ApiHttpHeaderFilter>();

                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                options.IncludeXmlComments(Path.Combine(basePath, "HJie.Identity.SwaggerGen.xml"), true);
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
            //启用静态文件中间件
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
                string _basePath = "";
                if (!string.IsNullOrWhiteSpace(_basePath))
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.BasePath = _basePath);
                }
            });

            //启用Swagger Bootstrap UI
            app.UseSwaggerBootstrapUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

            app.UseMvc();
        }
    }
}
