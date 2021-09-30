using Flix_Tv.Application.DatabaseInterfaces;
using Flix_Tv.Application.Services;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Flix_Tv.Persistence.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site
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
            services.AddControllersWithViews();

            #region IOC
            services.AddScoped<IFlixTvContext,FlixTvContext>();
            services.AddScoped<IViewRenderService, RenderViewToString>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ISerialService, SerialService>();
            services.AddScoped<IMovieService, MovieService>();
            #endregion
            #region DataBase
            services.AddDbContext<Persistence.Context.FlixTvContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FlixTvConnection")));
            #endregion

            #region Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(1000);

            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke();
                if (context.Response.StatusCode==404)
                {
                      context.Response.Redirect("/Error");
                }
            });

            app.Use(async (context, next) => {
             
                if (context.Request.Path.Value.ToString().ToLower().Contains("files")&& context.Request.Path.Value.ToString().ToLower().Contains("movies"))
                {

                    //var path = context.Request.Path.Value.ToString().ToLower();
                    // var a= context.Request.Query["catId"];
                    //   context.Response.Redirect("https://localhost:44308/CheckMovieFile?f="+ context.Request.Query["f"]+"&q="+ context.Request.Query["q"]);
                    var callingUrl = context.Request.Headers["Referer"].ToString();

                    if (callingUrl != "" && (callingUrl.ToLower().StartsWith("https://localhost:44308/checkmoviefile") || callingUrl.ToLower().StartsWith("http://localhost:44308/checkmoviefile")|| callingUrl.ToLower().StartsWith("https://localhost:44308/admin/getmoviefile") || callingUrl.ToLower().StartsWith("http://localhost:44308/admin/getmoviefile")))
                    {
                        await next.Invoke();
                    }
                    else
                    {
                    context.Response.Redirect("/");
                    }
                   // await next.Invoke();
                }
                else
                {
                    await next.Invoke();
                }
            }

       );
            app.Use(async (context, next) => {

                if (context.Request.Path.Value.ToString().ToLower().Contains("files") && context.Request.Path.Value.ToString().ToLower().Contains("serials"))
                {
                    //var path = context.Request.Path.Value.ToString().ToLower();
                    // var a= context.Request.Query["catId"];
                    //   context.Response.Redirect("https://localhost:44308/CheckMovieFile?f="+ context.Request.Query["f"]+"&q="+ context.Request.Query["q"]);
                    var callingUrl = context.Request.Headers["Referer"].ToString();

                    if (callingUrl != "" && (callingUrl.ToLower().StartsWith("https://localhost:44308/checkepisodefile") || callingUrl.ToLower().StartsWith("http://localhost:44308/checkepisodefile/")|| callingUrl.ToLower().StartsWith("https://localhost:44308/admin/getepisodefile") || callingUrl.ToLower().StartsWith("http://localhost:44308/admin/getepisodefile")))
                    {
                        await next.Invoke();
                    }
                    else
                    {
                        context.Response.Redirect("/");
                    }
                    // await next.Invoke1();
                }
                else
                {
                    await next.Invoke();
                }
            }

     );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
               name: "areas",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
             );
            });

        }
    }
}
