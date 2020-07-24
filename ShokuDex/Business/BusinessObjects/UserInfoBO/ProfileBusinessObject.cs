using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.UserInfo;
using Recodme.ShokuDex.DataAccess.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO
{
    public class ProfilesBusinessObject
    {
        private BaseDataAccessObject<Profiles> _dao;

        public ProfilesBusinessObject()
        {
            _dao = new BaseDataAccessObject<Profiles>();
        }

        private TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region C

        public OperationResult Create(Profiles profile)
        {
            try
            {
                _dao.Create(profile);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> CreateAsync(Profiles profile)
        {
            try
            {
                await _dao.CreateAsync(profile);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
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
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = _dao.Read(id);
                    scope.Complete();
                    return new OperationResult<Profiles>() { Success = true, Result = res };
                }
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
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _dao.ReadAsync(id);
                    scope.Complete();
                    return new OperationResult<Profiles>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<Profiles>() { Success = true, Exception = e };
            }
        }

        #endregion

        #region U

        public OperationResult Update(Profiles profile)
        {
            try
            {
                _dao.Update(profile);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> UpdateAsync(Profiles profile)
        {
            try
            {
                await _dao.UpdateAsync(profile);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region D

        public OperationResult Delete(Profiles profile)
        {
            try
            {
                _dao.Delete(profile);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> DeleteAsync(Profiles profile)
        {
            try
            {
                await _dao.DeleteAsync(profile);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public OperationResult Delete(Guid id)
        {
            try
            {
                _dao.Delete(id);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            try
            {
                await _dao.DeleteAsync(id);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        #endregion


        #region List

        public OperationResult<List<Profiles>> List()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                    var res = (List<Profiles>)list.Where(x => !x.IsDeleted);
                    scope.Complete();
                    return new OperationResult<List<Profiles>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profiles>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> ListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = await _dao.ListAsync();
                    var res = (List<Profiles>)list.Where(x => !x.IsDeleted);
                    scope.Complete();
                    return new OperationResult<List<Profiles>>() { Success = true, Result = res };
                }

            }

            catch (Exception e)
            {
                return new OperationResult<List<Profiles>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
