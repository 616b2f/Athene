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
using Athene.Abstractions;

namespace Athene.Inventory.Web
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
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
                options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("Librarian", policy => policy.RequireRole("Librarian", "Administrator"));
            });

            services.AddDistributedMemoryCache();
            services.AddSession();

            // Add application services.
            services.AddIdentity<ApplicationUser, IdentityRole>();
            services.AddSingleton<IInventory>(new Athene.Abstractions.TestImp.InMemoryInventory());
            services.AddSingleton<IArticleRepository>(new Athene.Abstractions.TestImp.InMemoryArticleRepository());
            services.AddSingleton<IUserRepository>(new Athene.Abstractions.TestImp.InMemoryUserRepository());

            services.AddSingleton<IUserStore<ApplicationUser>, InMemoryStore<ApplicationUser, IdentityRole>>();
            services.AddSingleton<IRoleStore<IdentityRole>, InMemoryStore<ApplicationUser, IdentityRole>>();
            services.AddSingleton<IUserEmailStore<ApplicationUser>, InMemoryStore<ApplicationUser, IdentityRole>>();
            services.AddSingleton<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();
            // services.AddSingleton<IUserStore<ApplicationUser>>(new AtheneUserStore<ApplicationUser>());
            // services.AddSingleton<IRoleStore<ApplicationUser>>(new AtheneRoleStore<ApplicationUser>());
            services.AddSingleton<UserManager<ApplicationUser>>();
            services.AddSingleton<RoleManager<IdentityRole>>();
            services.AddSingleton<SignInManager<ApplicationUser>>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

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

                // Cookie settings
                options.Cookies.ApplicationCookie.AuthenticationScheme = "Cookies";
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

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

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
            });

            app.UseSession();
            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseIdentity();
            TestData.CreateUsers(userManager);

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
