using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HJie.Identity.ID4.Data;

namespace HJie.Identity.ID4
{
    public class Startup
    {
        public IConfiguration _cfg { get; }
        public Startup(IConfiguration configuration)
        {
            _cfg = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //var connectionString = _cfg.GetConnectionString("DefaultConnection") ?? "";

            //services.AddDbContext<CustomApplicationDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));

            //services.AddIdentityServer()
            //        .AddDeveloperSigningCredential()
            //        .AddConfigurationStore<CustomConfigurationDbContext>(options =>
            //        {
            //            options.ConfigureDbContext = builder =>
            //              builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            //        })
            //        .AddOperationalStore<CustomPersistedGrantDbContext>(options =>
            //        {
            //            options.ConfigureDbContext = builder =>
            //              builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));

            //            // this enables automatic token cleanup. this is optional.
            //            options.EnableTokenCleanup = true;
            //            options.TokenCleanupInterval = 30;
            //        });
            ////MVC的相关服务
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddSession();




            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
           // in-memory, code config
           .AddTestUsers(InMemoryConfig.Users().ToList())
           .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
           .AddInMemoryClients(InMemoryConfig.GetClients());


            builder.AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();//认证
            app.UseAuthorization();//授权
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "account_default",
                    pattern: "{controller=account}/{action=login}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=account}/{action=index}/{id?}");
                endpoints.MapRazorPages();
            });
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
