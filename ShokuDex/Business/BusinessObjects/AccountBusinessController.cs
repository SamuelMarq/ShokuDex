using Microsoft.AspNetCore.Identity;
using Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO;
using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.UserInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects
{
    public class AccountBusinessController
    {
        private UserManager<User> UserManager { get; set; }
        private RoleManager<Role> RoleManager { get; set; }
        private readonly ProfilesBusinessObject _pbo = new ProfilesBusinessObject();

        public AccountBusinessController(UserManager<User> uManager, RoleManager<Role> rManager)
        {
            UserManager = uManager;
            RoleManager = rManager;
        }

        private TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        //public async Task<OperationResult> Register(string userName, string email, string password, Profiles profile, string role)
        //{
        //    if (await UserManager.FindByEmailAsync(email) != null)
        //        return new OperationResult() { Success = false, Message = $"User {email} already exists" };
        //    if (await UserManager.FindByNameAsync(userName) != null)
        //        return new OperationResult() { Success = false, Message = $"User {userName} already exists" };
        //    using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        //    try
        //    {
        //        var createPersonOperation = await _pbo.CreateAsync(profile);
        //        if (!createPersonOperation.Success)
        //        {
        //            transactionScope.Dispose();
        //            return createPersonOperation;
        //        }
        //        var admin = new User()
        //        {
        //            Email = email,
        //            UserName = userName,
        //            ProfileId = profile.Id
        //        };
        //        var result = await UserManager.CreateAsync(admin, password);
        //        if (!result.Succeeded)
        //        {
        //            transactionScope.Dispose();
        //            return new OperationResult() { Success = false, Message = result.ToString() };
        //        }
        //        var roleData = await RoleManager.FindByNameAsync(role);
        //        if (roleData == null)
        //        {
        //            transactionScope.Dispose();
        //            return new OperationResult() { Success = false, Message = $"Role {role} does not exist" };
        //        }
        //        transactionScope.Complete();
        //        return new OperationResult() { Success = true };
        //    }
        //    catch (Exception e)
        //    {
        //        return new OperationResult() { Success = false, Exception = e };
        //    }
        //}
    }
}
