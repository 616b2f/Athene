using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Athene.Abstractions.Models;

namespace Athene.Inventory.Web.Models 
{
    public class AtheneUserStore<TUser> : IUserStore<TUser> 
        where TUser : ApplicationUser
    {
        private readonly List<TUser> _users = new List<TUser>();
        public Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                _users.Add(user);
                return IdentityResult.Success;
            });
        }

        public Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                _users.Remove(user);
                return IdentityResult.Success;
            });
        }

        public void Dispose() { }

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
               return _users.SingleOrDefault(u => u.Id == userId);
            });
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
               return _users.SingleOrDefault(u => u.NormalizedUserName == normalizedUserName);
            });
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
               return _users.SingleOrDefault(u => u.Id == user.Id)?.NormalizedUserName;
            });
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
               return _users.SingleOrDefault(u => u.Id == user.Id)?.Id;
            });

        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
               return _users.SingleOrDefault(u => u.Id == user.Id)?.UserName;
            });
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
               var tmpUser = _users.SingleOrDefault(u => u.Id == user.Id);
               tmpUser.NormalizedUserName = normalizedName;
            });
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
               var tmpUser = _users.SingleOrDefault(u => u.Id == user.Id);
               tmpUser.UserName = userName;
            });
        }

        public Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                var tmpUser = _users.SingleOrDefault(u => u.Id == user.Id);
                if (tmpUser == null)
                {
                    return IdentityResult.Failed(new IdentityError 
                    {
                        Code = "",
                        Description = "User not found",
                    });
                }
                else
                {
                    tmpUser.UserName = user.UserName;
                    tmpUser.NormalizedUserName = user.NormalizedUserName;
                    tmpUser.Address = user.Address;
                    tmpUser.Birthsday = user.Birthsday;
                    tmpUser.Email = user.Email;
                    tmpUser.EmailConfirmed = user.EmailConfirmed;
                    tmpUser.Gender = user.Gender;
                    tmpUser.ConcurrencyStamp = user.ConcurrencyStamp;
                    return IdentityResult.Success;
                }
            });
        }
    }
}