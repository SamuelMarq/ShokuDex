using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.FoodInfo;
using Recodme.ShokuDex.DataAccess.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects.FoodInfoDAO
{
    public class CategoriesBusinessObject
    {
        private BaseDataAccessObject<Categories> _dao;

        public CategoriesBusinessObject()
        {
            _dao = new BaseDataAccessObject<Categories>();
        }

        private TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region C

        public OperationResult Create(Categories category)
        {
            try
            {
                _dao.Create(category);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> CreateAsync(Categories category)
        {
            try
            {
                await _dao.CreateAsync(category);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        #endregion

        #region R

        public OperationResult<Categories> Read(Guid id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled)) 
                { 
                    var res = _dao.Read(id);
                scope.Complete();
                return new OperationResult<Categories>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<Categories>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Categories>> ReadAsync(Guid id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _dao.ReadAsync(id);
                    scope.Complete();
                    return new OperationResult<Categories>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<Categories>() { Success = true, Exception = e };
            }
        }

        #endregion

        #region U

        public OperationResult Update(Categories category)
        {
            try
            {
                _dao.Update(category);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> UpdateAsync(Categories category)
        {
            try
            {
                await _dao.UpdateAsync(category);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region D

        public OperationResult Delete(Categories category)
        {
            try
            {
                _dao.Delete(category);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> DeleteAsync(Categories category)
        {
            try
            {
                await _dao.DeleteAsync(category);
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

        public OperationResult<List<Categories>> FullList()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                    scope.Complete();
                    return new OperationResult<List<Categories>>() { Success = true, Result = list };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Categories>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> FullListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = await _dao.ListAsync();
                    scope.Complete();
                    return new OperationResult<List<Categories>>() { Success = true, Result = list };
                }

            }

            catch (Exception e)
            {
                return new OperationResult<List<Categories>>() { Success = false, Exception = e };
            }
        }


        public OperationResult<List<Categories>> List()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                    var res = list.Where(x => !x.IsDeleted).ToList();
                    
                    scope.Complete();
                    return new OperationResult<List<Categories>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Categories>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Categories>>> ListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = await _dao.ListAsync();
                    var res = list.Where(x => !x.IsDeleted).ToList();
                    scope.Complete();
                    return new OperationResult<List<Categories>>() { Success = true, Result = res };
                }

            }

            catch (Exception e)
            {
                return new OperationResult<List<Categories>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
