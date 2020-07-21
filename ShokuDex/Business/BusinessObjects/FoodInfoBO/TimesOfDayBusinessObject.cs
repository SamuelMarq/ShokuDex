using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.FoodInfo;
using Recodme.ShokuDex.DataAccess.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO
{
    public class TimesOfDayBusinessObject
    {
        private BaseDataAccessObject<TimesOfDay> _dao;
        public TimesOfDayBusinessObject()
        {
            _dao = new BaseDataAccessObject<TimesOfDay>();
        }

        private TransactionOptions opts = new TransactionOptions()
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region Create
        public OperationResult Create(TimesOfDay item)
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

        public async Task<OperationResult> CreateAsync(TimesOfDay item)
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
        public OperationResult<TimesOfDay> Read(Guid id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = _dao.Read(id);
                    scope.Complete();
                    return new OperationResult<TimesOfDay>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<TimesOfDay>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<TimesOfDay>> ReadAsync(Guid id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _dao.ReadAsync(id);
                    scope.Complete();
                    return new OperationResult<TimesOfDay>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<TimesOfDay>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public OperationResult Update(TimesOfDay item)
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

        public async Task<OperationResult> UpdateAsync(TimesOfDay item)
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
        public OperationResult Delete(TimesOfDay item)
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

        public async Task<OperationResult> DeleteAsync(TimesOfDay item)
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
        public OperationResult<List<TimesOfDay>> FullList()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = _dao.List();
                    scope.Complete();
                    return new OperationResult<List<TimesOfDay>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<TimesOfDay>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<TimesOfDay>>> FullListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _dao.ListAsync();
                    scope.Complete();
                    return new OperationResult<List<TimesOfDay>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<TimesOfDay>>() { Success = false, Exception = e };
            }
        }

        public OperationResult<List<TimesOfDay>> List()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                    var res = list.Where(x => !x.IsDeleted).ToList();
                    scope.Complete();
                    return new OperationResult<List<TimesOfDay>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<TimesOfDay>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<TimesOfDay>>> ListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = await _dao.ListAsync();
                    var res = list.Where(x => !x.IsDeleted).ToList();
                    scope.Complete();
                    return new OperationResult<List<TimesOfDay>>() { Success = true, Result = res };
                }

            }

            catch (Exception e)
            {
                return new OperationResult<List<TimesOfDay>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter

        public async Task<OperationResult<List<TimesOfDay>>> FilterAsync(Func<TimesOfDay, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<TimesOfDay>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<TimesOfDay>>() { Success = false, Exception = e };
            }
        }

        #endregion
    }
}
