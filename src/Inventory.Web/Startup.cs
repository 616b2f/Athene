using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Athene.Inventory;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Data;
using Athene.Inventory.Data.Contexts;
using Athene.Inventory.Data.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Localization.JsonLocalizer.StringLocalizer;
using System;

namespace Inventory.Web
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
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Inventory.Web")));

            services.AddDefaultIdentity<User>(opt => {
                    opt.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                    opt.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                    opt.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<InventoryDbContext>();

            services.AddAthenePolicies();

            services.AddDistributedMemoryCache();
            services.AddSession();

            // Add application services.
            services.AddTransient<IInventoryProvider, InventoryProvider>();
            services.AddTransient<IArticleProvider, ArticleProvider>();
            services.AddTransient<IBookMetaProvider, BookMetaProvider>();
            services.AddTransient<IUserProvider<User>, UserProvider>();

            services.AddTransient<IUnitOfWork<User>, UnitOfWork>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, InventoryDbContext>(opt => {
                    opt.Clients["Inventory.Web"].AlwaysIncludeUserClaimsInIdToken = true;
                    if (!opt.ApiResources["Inventory.WebAPI"].UserClaims.Contains(JwtClaimTypes.Role))
                    {
                        opt.ApiResources["Inventory.WebAPI"].UserClaims
                            .Add(JwtClaimTypes.Role);
                    }
                });

            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddAuthentication()
                .AddIdentityServerJwt();

            // services.Configure<JwtBearerOptions>(
            //     IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
            //     options =>
            //     {
            //         var onTokenValidated = options.Events.OnTokenValidated;       
            //         options.Events.OnTokenValidated = async (context) =>
            //         {
            //             // var user = context.Principal.Identity.;
            //             await onTokenValidated(context);
            //         };
            //     });
            services.AddJsonLocalization(opt => opt.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                string defaultCulture = "en";
                opt.DefaultRequestCulture = new RequestCulture(defaultCulture);
                // // Formatting numbers, dates, etc.
                // SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                var supportedCultures = new[]
                {
                    new CultureInfo(defaultCulture),
                    new CultureInfo("de"),
                };
                opt.SupportedUICultures = supportedCultures;
            });

            services.AddControllersWithViews()
                    .AddJsonOptions(opt => {
                        opt.JsonSerializerOptions.Converters.Add(new AbstractWriteOnlyJsonConverter());
                    });
                
            services.AddRazorPages()
                    .AddMvcLocalization();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(opt => 
            {
                opt.GeneratePolymorphicSchemas();
                opt.SwaggerDoc("v1", 
                    new OpenApiInfo { 
                        Title = "Athene Inventory API", 
                        Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseSwaggerUI(x => {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Athene Inventory API");
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
