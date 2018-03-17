using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Athene.Inventory.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Web.Models
{
    public class InMemoryStore<TUser, TRole> :
        IUserStore<TUser>,
        IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IQueryableRoleStore<TRole>,
        IUserEmailStore<TUser>,
        IUserRepository
        where TRole : IdentityRole
        where TUser : ApplicationUser
    {
        private readonly Dictionary<string, TUser> _logins = new Dictionary<string, TUser>();
        private readonly Dictionary<string, TUser> _users = new Dictionary<string, TUser>();
        private readonly Dictionary<string, TRole> _roles = new Dictionary<string, TRole>();
        public IQueryable<TUser> Users
        {
            get { return _users.Values.AsQueryable(); }
        }

        public IQueryable<TRole> Roles
        {
            get { return _roles.Values.AsQueryable(); }
        }

        private string GetLoginKey(string loginProvider, string providerKey)
        {
            return loginProvider + "|" + providerKey;
        }

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            user.Logins.Add(new IdentityUserLogin<string>
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            });
            _logins[GetLoginKey(login.LoginProvider, login.ProviderKey)] = user;
            return Task.FromResult(0);
        }

        public Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            _users[user.Id] = user;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            _users[user.Id] = user;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null || !_users.ContainsKey(user.Id))
            {
                throw new InvalidOperationException("Unknown user");
            }
            _users.Remove(user.Id);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_users.ContainsKey(userId))
            {
                return Task.FromResult(_users[userId]);
            }
            return Task.FromResult<TUser>(null);
        } 

        public Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            string key = GetLoginKey(loginProvider, providerKey);
            if (_logins.ContainsKey(key))
            {
                return Task.FromResult(_logins[key]);
            }
            return Task.FromResult<TUser>(null);
        }

        public Task<TUser> FindByNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return
                Task.FromResult(
                    Users.FirstOrDefault(u => u.NormalizedUserName == userName));
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            IList<UserLoginInfo> result = user.Logins
                .Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToList();
            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(user.UserName);
        }

        public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var loginEntity =
                user.Logins.SingleOrDefault(
                    l =>
                        l.ProviderKey == providerKey && l.LoginProvider == loginProvider &&
                        l.UserId == user.Id);
            if (loginEntity != null)
            {
                user.Logins.Remove(loginEntity);
            }
            _logins[GetLoginKey(loginProvider, providerKey)] = null;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.NormalizedUserName = userName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
        }

        // RoleId == roleName for InMemory
        public Task AddToRoleAsync(TUser user, string role, CancellationToken cancellationToken = default(CancellationToken))
        {
            var roleEntity = _roles.Values.SingleOrDefault(r => r.NormalizedName == role);
            if (roleEntity != null)
            {
                user.Roles.Add(new IdentityUserRole<string> { RoleId = roleEntity.Id, UserId = user.Id });
            }
            return Task.FromResult(0);
        }

        // RoleId == roleName for InMemory
        public Task RemoveFromRoleAsync(TUser user, string role, CancellationToken cancellationToken = default(CancellationToken))
        {
            var roleObject = _roles.Values.SingleOrDefault(r => r.NormalizedName == role);
            var roleEntity = user.Roles.SingleOrDefault(ur => ur.RoleId == roleObject.Id);
            if (roleEntity != null)
            {
                user.Roles.Remove(roleEntity);
            }
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            IList<string> roles = new List<string>();
            foreach (var r in user.Roles.Select(ur => ur.RoleId))
            {
                roles.Add(_roles[r].Name);
            }
            return Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(TUser user, string role, CancellationToken cancellationToken = default(CancellationToken))
        {
            var roleObject = _roles.Values.SingleOrDefault(r => r.NormalizedName == role);
            bool result = roleObject != null && user.Roles.Any(ur => ur.RoleId == roleObject.Id);
            return Task.FromResult(result);
        }

        // RoleId == rolename for inmemory store tests
        public Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var role = _roles.Values.Where(x => x.NormalizedName.Equals(roleName)).SingleOrDefault();
            if (role == null)
            {
                return Task.FromResult<IList<TUser>>(new List<TUser>());
            }
            return Task.FromResult<IList<TUser>>(Users.Where(u => (u.Roles.Where(x => x.RoleId == role.Id).Count() > 0)).Select(x => x).ToList());
        }

        public Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            _roles[role.Id] = role;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            _roles[role.Id] = role;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (role == null || !_roles.ContainsKey(role.Id))
            {
                throw new InvalidOperationException("Unknown role");
            }
            _roles.Remove(role.Id);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        Task<TRole> IRoleStore<TRole>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (_roles.ContainsKey(roleId))
            {
                return Task.FromResult(_roles[roleId]);
            }
            return Task.FromResult<TRole>(null);
        }

        Task<TRole> IRoleStore<TRole>.FindByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return
                Task.FromResult(
                    Roles.SingleOrDefault(r => String.Equals(r.NormalizedName, roleName, StringComparison.OrdinalIgnoreCase)));
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                Users.SingleOrDefault(u => u.NormalizedEmail == normalizedEmail));
        }

        public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            var claims = user.Claims.Select(c => c.ToClaim()).ToList();
            return Task.FromResult<IList<Claim>>(claims);
        }

        public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                user.Claims.Add(new IdentityUserClaim<string> { ClaimType = claim.Type, ClaimValue = claim.Value, UserId = user.Id });
            }
            return Task.FromResult(0);
        }

        public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            var matchedClaims = user.Claims.Where(uc => uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList();
            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimValue = newClaim.Value;
                matchedClaim.ClaimType = newClaim.Type;
            }
            return Task.FromResult(0);
        }

        public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                var entity =
                    user.Claims.FirstOrDefault(
                        uc => uc.UserId == user.Id && uc.ClaimType == claim.Type && uc.ClaimValue == claim.Value);
                if (entity != null)
                {
                    user.Claims.Remove(entity);
                }
            }
            return Task.FromResult(0);
        }

        public Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            var query = from user in Users
                        where user.Claims.Where(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value).FirstOrDefault() != null
                        select user;

            return Task.FromResult<IList<TUser>>(query.ToList());
        }

        public IEnumerable<IUser> FindByMatchcode(string matchcode)
        {
            var normalizedMatchcode = matchcode.ToLower();
            return Users
                .Where(u => 
                    u.FullName.ToLower().Contains(normalizedMatchcode) ||
                    (u.StudentId != null &&
                     u.StudentId.Contains(normalizedMatchcode)))
                .ToArray();
        }

        public IUser FindByUserId(string userId)
        {
            return Users.SingleOrDefault(u => u.Id.ToString() == userId);
        }

        public void Add(IUser user)
        {
            if (user.Id == null)
                user.Id = Guid.NewGuid().ToString();
            _users[user.Id] = (TUser)user;
        }
    }
}