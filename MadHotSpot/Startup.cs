using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tik4net.Objects.User;
using MadHotSpot.Interfaces;
using MadHotSpot.Extentions;

namespace MadHotSpot
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region DbContext
            services.AddDbContext<OtelAppDbContext>(options =>  options.UseSqlServer(Configuration.GetConnectionString("OtelAppDatabase")));
            #endregion

            services.AddScoped<IFileUpload, FileUpload>();

            services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 2;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Lockout = new LockoutOptions { AllowedForNewUsers = false };
                    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                })
                .AddEntityFrameworkStores<OtelAppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });


            services.ConfigureApplicationCookie(options =>
            {
                //options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                //options.Cookie = new CookieBuilder
                //{
                //    IsEssential = true, // required for auth to work without explicit user consent; adjust to suit your privacy policy,
                //    Name = ".EfaturaPortal.Session",
                //    HttpOnly = false,
                //    SecurePolicy = CookieSecurePolicy.Always
                //};

            });


            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(20);
            //    options.Cookie.HttpOnly = false;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //    options.Cookie.Name = ".OtelApp.Session";
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "AllowOrigin",
            //        builder =>
            //        {
            //            builder.WithOrigins("https://localhost:44351", "http://localhost:5001")
            //                                .AllowAnyHeader()
            //                                .AllowCredentials()
            //                                .AllowAnyOrigin()
            //                                .AllowAnyMethod();
            //        });
            //});



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            ////app.UseHttpsRedirection();
            //app.UseStaticFiles();
            ////app.UseCookiePolicy();
            //app.UseAuthentication();

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "usernotfound",
                    pattern: "/usernotfound",
                    defaults: new { controller = "Error", action = "UserNotFound" });

                endpoints.MapControllerRoute(
                    name: "notfound",
                    pattern: "/notfound",
                    defaults: new { controller = "Error", action = "NotFound" });

                endpoints.MapControllerRoute(
                    name: "unauthorized",
                    pattern: "/unauthorized",
                    defaults: new { controller = "Auth", action = "Unauthorized" });

                endpoints.MapControllerRoute(
                    name: "authentication",
                    pattern: "/authentication",
                    defaults: new { controller = "Auth", action = "Login" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCors("AllowOrigin");
        }
    }
}
