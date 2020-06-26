using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.UserInfo;
using Recodme.ShokuDex.DataAccess.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects.UserInfoDAO
{
    public class ProfileBusinessObject
    {
        private BaseDataAccessObject<Profiles> _dao;

        public ProfileBusinessObject()
        {
            _dao = new BaseDataAccessObject<Profiles>();
        }

        private TransactionOptions opts = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region C

        public OperationResult Create(Profiles item)
        {
            try
            {
                var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(item);
                scope.Complete();
                return new OperationResult() { Success = true };
            }
            catch(Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> CreateAsync(Profiles item)
        {
            try
            {
                var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.Create(item);
                scope.Complete();
                return new OperationResult() { Success = true };
            }
            catch(Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        #endregion

        #region R

        public OperationResult<Profiles> Read(Guid id)
        {
            try
            {
                var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled);
                var res = _dao.Read(id);
                scope.Complete();
                return new OperationResult<Profiles>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<Profiles>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Profiles>> ReadAsync(Guid id)
        {
            try
            {
                var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled);
                var res = await _dao.ReadAsync(id);
                scope.Complete();
                return new OperationResult<Profiles>() { Success = true, Result = res }; 
            }
        }
        #endregion
    }
}
