using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.FoodInfo;
using Recodme.ShokuDex.DataAccess.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO
{
    public class MealsBusinessObject
    {
        private BaseDataAccessObject<Meals> _dao;
        public MealsBusinessObject()
        {
            _dao = new BaseDataAccessObject<Meals>();
        }

        private TransactionOptions opts = new TransactionOptions()
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region Create
        public OperationResult Create(Meals item)
        {
            try
            {
                _dao.Create(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> CreateAsync(Meals item)
        {
            try
            {
                await _dao.CreateAsync(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read
        public OperationResult<Meals> Read(Guid id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = _dao.Read(id);
                    scope.Complete();
                    return new OperationResult<Meals>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<Meals>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Meals>> ReadAsync(Guid id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _dao.ReadAsync(id);
                    scope.Complete();
                    return new OperationResult<Meals>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<Meals>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public OperationResult Update(Meals item)
        {
            try
            {
                _dao.Update(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }

        }

        public async Task<OperationResult> UpdateAsync(Meals item)
        {
            try
            {
                await _dao.UpdateAsync(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public OperationResult Delete(Meals item)
        {
            try
            {
                _dao.Delete(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }

        }

        public async Task<OperationResult> DeleteAsync(Meals item)
        {
            try
            {
                await _dao.DeleteAsync(item);
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
        public OperationResult<List<Meals>> List()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = _dao.List();
                    scope.Complete();
                    return new OperationResult<List<Meals>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meals>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Meals>>> ListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _dao.ListAsync();
                    scope.Complete();
                    return new OperationResult<List<Meals>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meals>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
