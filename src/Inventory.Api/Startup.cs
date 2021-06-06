using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Identity;
using Athene.Inventory.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Data.Contexts;
using Athene.Inventory.Data;
using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Data.Services;
using Microsoft.Extensions.Hosting;

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
            // services.Configure<CookiePolicyOptions>(options =>
            // {
            //     // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //     options.CheckConsentNeeded = context => true;
            //     options.MinimumSameSitePolicy = SameSiteMode.None;
            // });

            // Add framework services.
            string selectedDb = Configuration.GetValue("Database", "none");
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            if (selectedDb == Constants.Databases.Sqlite)
            {
                services.AddDbContext<InventoryDbContext>(options =>
                    options.UseSqlite(connectionString));
            }
            else if (selectedDb == Constants.Databases.MySql)
            {
                services.AddDbContext<InventoryDbContext>(builder =>
                    builder.UseMySql(connectionString, opt => {
                        // opt.MigrationsAssembly(migrationsAssembly);
                        opt.EnableRetryOnFailure();
                    })
                );
            }

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<InventoryDbContext>()
                .AddDefaultTokenProviders();

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

            // add in memory user store
            // var store = new InMemoryStore<ApplicationUser, IdentityRole>();
            // services.AddTransient<IUserRepository, UserRepository>();
            // services.AddSingleton<IUserStore<ApplicationUser>>(store);
            // services.AddSingleton<IRoleStore<IdentityRole>>(store);
            // services.AddSingleton<IUserEmailStore<ApplicationUser>>(store);
            // services.AddSingleton<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();
            // services.AddScoped<UserManager<ApplicationUser>>();
            // services.AddScoped<RoleManager<IdentityRole>>();
            // services.AddScoped<SignInManager<ApplicationUser>>();

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

            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options => {
            //         options.ExpireTimeSpan = TimeSpan.FromDays(150);
            //         options.LoginPath = "/Account/Login";
            //         options.LogoutPath = "/Account/Logoff";
            //     });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
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

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

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
