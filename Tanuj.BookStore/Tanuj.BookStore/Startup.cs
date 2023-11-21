using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tanuj.BookStore.Data;
using Tanuj.BookStore.Models;
using Tanuj.BookStore.Repository;

namespace Tanuj.BookStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)   // using dependency injection. IConfiguration to access settings.json 
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // For adding dependencies and services
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.;Database=BookStore1;Integrated Security=True;"));
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));  // getting connection string from appsetting.json


            services.AddIdentity<IdentityUser, IdentityRole >().AddEntityFrameworkStores<BookStoreContext>();  // configuring identity core framework to work with database

            services.AddControllersWithViews();  // adding mvc design pattern


            // preprocessor directive 
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();   // so that we dont have to run again and again after every edit
            //    .AddViewOptions(option =>
            //{
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;

            //}); 
#endif
            services.AddScoped<IBookRepository, BookRepository>();  // for dependency injection registring service in container
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.Configure<NewBookAlertConfig>(_configuration.GetSection("NewBookAlert"));   // using Ioption to access apps.setting.json
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

            app.UseAuthentication();    // using identity core framework

            // maping a usl to a particular resource 
               app.UseEndpoints(endpoints =>
               {
                   /* endpoints.Map("/", async context =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    });

                    */

                   endpoints.MapDefaultControllerRoute();


                   //endpoints.MapControllerRoute(
                   //   name: "Default",
                   //   pattern: "bookApp/{controller=Home}/{action=Index}/{id?}");


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
