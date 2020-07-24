using Microsoft.AspNetCore.Identity;
using Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO;
using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.UserInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO
{
    public class AccountBusinessObject
    {
        private UserManager<User> UserManager { get; set; }
        private RoleManager<ShokuDexRole> RoleManager { get; set; }
        private readonly ProfilesBusinessObject _pbo = new ProfilesBusinessObject();

        public AccountBusinessObject(UserManager<User> uManager, RoleManager<ShokuDexRole> rManager)
        {
            UserManager = uManager;
            RoleManager = rManager;
        }

        private TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public async Task<OperationResult> Register(string userName, string password, Profiles profile, string role)
        {
            if (await UserManager.FindByEmailAsync(profile.Email) != null)
                return new OperationResult() { Success = false, Message = $"User {profile.Email} already exists" };
            if (await UserManager.FindByNameAsync(userName) != null)
                return new OperationResult() { Success = false, Message = $"User {userName} already exists" };
            using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var createPersonOperation = await _pbo.CreateAsync(profile);
                if (!createPersonOperation.Success)
                {
                    transactionScope.Dispose();
                    return createPersonOperation;
                }
                var admin = new User()
                {
                    Email = profile.Email,
                    UserName = userName,
                    ProfileId = profile.Id
                };
                var result = await UserManager.CreateAsync(admin, password);
                if (!result.Succeeded)
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = result.ToString() };
                }
                var roleData = await RoleManager.FindByNameAsync(role);
                if (roleData == null)
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = $"Role {role} does not exist" };
                }
                transactionScope.Complete();
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Profiles>> GetProfile(IdentityUser<int> user)
        {
            if (user is User)
            {
                var restUser = (User)user;
                try
                {

                    using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                    var profileOperation = await _pbo.ReadAsync(restUser.ProfileId);
                    transactionScope.Complete();
                    return profileOperation;
                }
                catch (Exception e)
                {
                    return new OperationResult<Profiles>() { Success = false, Exception = e };
                }
            }
            return new OperationResult<Profiles>() { Success = false, Message = "The user is not a User" };
        }
    }
}
