using System.Security.Claims;
using Athene.Inventory.Web;
using IdentityModel;

namespace Microsoft.Extensions.DependencyInjection
{
    
    public static class AtheneServiceCollectionExtensions 
    {
        public static IServiceCollection AddAthenePolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.Administrator, policy => policy.RequireRole(Constants.Roles.Administrator));
                options.AddPolicy(Constants.Policies.Librarian, policy => policy.RequireRole(Constants.Roles.Administrator, Constants.Roles.Librarian));
                options.AddPolicy(Constants.Policies.AdministrateInventory, policy => 
                    policy.RequireClaim(Constants.ClaimTypes.Permission, Constants.Permissions.AdministrateInventory));
                options.AddPolicy(Constants.Policies.DataImport, policy => 
                    policy.RequireClaim(Constants.ClaimTypes.Permission, Constants.Permissions.DataImport));
            });
            return services;
        }
    }
}