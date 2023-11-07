using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tanuj.BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
       
        // For adding dependencies and services
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();  // adding mvc design pattern


// preprocessor directive 
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();  // so that we dont have to run again and again after every edit
#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // For http pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();  // middleware

            
            // to use static files other than from wwwroot
          /*  app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory() , "MyStaticFiles")),
                RequestPath = "/MyStaticFiles"
            });
          */

            //map a particular url to a resource
               app.UseRouting();

            // maping a usl to a particular resource 
               app.UseEndpoints(endpoints =>
               {
                  /* endpoints.Map("/", async context =>
                   {
                       await context.Response.WriteAsync("Hello World!");
                   });

                   */

                   endpoints.MapDefaultControllerRoute();
               });

           /* 
            
            app.UseEndpoints(endpoints =>
            {
                  endpoints.Map("/Tanuj", async context =>
                  {
                      await context.Response.WriteAsync("Hello Tanuj!");
                  });
                

                
            });

            */

        }
    }
}
