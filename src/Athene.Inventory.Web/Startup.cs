using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Identity;
using Athene.Inventory.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace Athene.Inventory.Web
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // services.AddDbContext<InventoryDbContext>(options =>
            //     options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddEntityFrameworkStores<InventoryDbContext>()
            //     .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.Administrator, policy => policy.RequireRole(Constants.Roles.Administrator));
                options.AddPolicy(Constants.Policies.Librarian, policy => 
                    policy.RequireRole(Constants.Roles.Librarian, Constants.Roles.Administrator));
                options.AddPolicy(Constants.Policies.AdministrateInventory, policy => 
                    policy.RequireClaim(Constants.ClaimTypes.Permission, Constants.Permissions.AdministrateInventory));
                options.AddPolicy(Constants.Policies.DataImport, policy => 
                    policy.RequireClaim(Constants.ClaimTypes.Permission, Constants.Permissions.DataImport));
            });
            services.AddScoped<IAuthorizationService, DefaultAuthorizationService>();

            services.AddDistributedMemoryCache();
            services.AddSession();

            // Add application services.
            services.AddIdentity<ApplicationUser, IdentityRole>();
            services.AddSingleton<IInventory, Athene.Inventory.Abstractions.TestImp.InMemoryInventory>();
            services.AddSingleton<IArticleRepository, Athene.Inventory.Abstractions.TestImp.InMemoryArticleRepository>();
            services.AddSingleton<IBookMetaRepository, Athene.Inventory.Abstractions.TestImp.InMemoryBookMetaRepository>();

            var store = new InMemoryStore<ApplicationUser, IdentityRole>();
            services.AddSingleton<IUserRepository>(store);
            services.AddSingleton<IUserStore<ApplicationUser>>(store);
            services.AddSingleton<IRoleStore<IdentityRole>>(store);
            services.AddSingleton<IUserEmailStore<ApplicationUser>>(store);
            services.AddSingleton<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<SignInManager<ApplicationUser>>();

            services.AddScoped<IEmailSender, AuthMessageSender>();
            services.AddScoped<ISmsSender, AuthMessageSender>();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddJsonLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.ExpireTimeSpan = TimeSpan.FromDays(150);
                    options.LoginPath = "/Account/LogIn";
                    options.LogoutPath = "/Account/LogOff";
                });

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                string defaultCulture = "en";
                var supportedCultures = new[]
                {
                    new CultureInfo(defaultCulture),
                    new CultureInfo("de"),
                };

                opt.DefaultRequestCulture = new RequestCulture(defaultCulture);
                // // Formatting numbers, dates, etc.
                // SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                opt.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // app.UseCookieAuthentication(new CookieAuthenticationOptions
            // {
            //     AuthenticationScheme = "Cookies",
            //     AutomaticAuthenticate = true,
            //     AutomaticChallenge = true,
            // });

            app.UseSession();
            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseAuthentication();

            //UserManager<ApplicationUser> userManager
            // db.Database.EnsureCreated();
            // if (db.Books.Count() == 0) {
            //     db.Initialize(userManager);
            // }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoutes",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
