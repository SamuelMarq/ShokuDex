using Recodme.ShokuDex.Business.OperationResults;
using Recodme.ShokuDex.Data.FoodInfo;
using Recodme.ShokuDex.DataAccess.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO
{
    public class FoodsBusinessObject
    {
        private BaseDataAccessObject<Foods> _dao;

        public FoodsBusinessObject()
        {
            _dao = new BaseDataAccessObject<Foods>();
        }

        private TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region C

        public OperationResult Create(Foods food)
        {
            try
            {
                _dao.Create(food);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> CreateAsync(Foods food)
        {
            try
            {
                await _dao.CreateAsync(food);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        #endregion

        #region R

        public OperationResult<Foods> Read(Guid id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled)) 
                {
                    var res = _dao.Read(id);
                    scope.Complete();
                    return new OperationResult<Foods>() { Success = true, Result = res };
                }
                  
            }
            catch (Exception e)
            {
                return new OperationResult<Foods>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Foods>> ReadAsync(Guid id)
        {
            try
            {
                var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var res = await _dao.ReadAsync(id);
                scope.Complete();
                return new OperationResult<Foods>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<Foods>() { Success = true, Exception = e };
            }
        }

        #endregion

        #region U

        public OperationResult Update(Foods food)
        {
            try
            {
                _dao.Update(food);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> UpdateAsync(Foods food)
        {
            try
            {
                await _dao.UpdateAsync(food);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region D

        public OperationResult Delete(Foods food)
        {
            try
            {
                _dao.Delete(food);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> DeleteAsync(Foods food)
        {
            try
            {
                await _dao.DeleteAsync(food);
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

        public OperationResult<List<Foods>> List()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                    var res = (List<Foods>)list.Where(x => !x.IsDeleted);
                    scope.Complete();
                    return new OperationResult<List<Foods>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Foods>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> ListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = await _dao.ListAsync();
                    var res = (List<Foods>)list.Where(x =>!x.IsDeleted);
                    scope.Complete();
                    return new OperationResult<List<Foods>>() { Success = true, Result = res };
                }

            }

            catch (Exception e)
            {
                return new OperationResult<List<Foods>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region Find

        public OperationResult<List<Foods>> Find(string searchFood, Guid idCategory)
        {
            try
            {
                
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                  
                    var regex = new Regex("^" + searchFood);
                    var res = (List<Foods>)list.Where(x => regex.IsMatch(x.Name) && !x.IsDeleted); 
                    scope.Complete();
                    return new OperationResult<List<Foods>>() { Success = true, Result = res };
                }

            }
            catch (Exception e)
            {
                return new OperationResult<List<Foods>>() { Success = false, Exception = e };
            }
        }

        #endregion

    }
}
