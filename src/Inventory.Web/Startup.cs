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
using Microsoft.AspNetCore.Authorization;

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
            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlite(
            //         Configuration.GetConnectionString("DefaultConnection"),
            //         x => x.MigrationsAssembly("Inventory.Web")));

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Inventory.Web")));

            // services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<InventoryDbContext>();

            // services.AddIdentity<User, IdentityRole>()
            //     .AddEntityFrameworkStores<InventoryDbContext>()
            //     .AddDefaultTokenProviders();

            services.AddAthenePolicies();
            services.AddTransient<IAuthorizationService, DefaultAuthorizationService>();

            services.AddDistributedMemoryCache();
            services.AddSession();

            // Add application services.
            services.AddTransient<IInventoryProvider, InventoryProvider>();
            services.AddTransient<IArticleProvider, ArticleProvider>();
            services.AddTransient<IBookMetaProvider, BookMetaProvider>();
            services.AddTransient<IUserProvider<User>, UserProvider>();

            services.AddTransient<IUnitOfWork<User>, UnitOfWork>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, InventoryDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                    pattern: "{controller}/{action=Index}/{id?}");
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
