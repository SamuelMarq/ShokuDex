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
                if (food.Name == string.Empty) throw new Exception();

                    food.Calories = food.Fats * 9 + food.Carbohydrates * 4 + food.Protein * 4 + food.Alcohol * 7;
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
                food.Calories = food.Fats * 9 + food.Carbohydrates * 4 + food.Protein * 4 + food.Alcohol * 7;
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

        public OperationResult<List<Foods>> FullList()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                    scope.Complete();
                    return new OperationResult<List<Foods>>() { Success = true, Result = list };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Foods>>() { Success = false, Exception = e };
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
                    return new OperationResult<List<Foods>>() { Success = true, Result = list };
                }

            }

            catch (Exception e)
            {
                return new OperationResult<List<Foods>>() { Success = false, Exception = e };
            }
        }


        public OperationResult<List<Foods>> List()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = _dao.List();
                    var res = list.Where(x => !x.IsDeleted).ToList();
                    scope.Complete();
                    return new OperationResult<List<Foods>>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Foods>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Foods>>> ListAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = await _dao.ListAsync();
                    var res = list.Where(x => !x.IsDeleted).ToList();
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

        #region Filter

        public async Task<OperationResult<List<Foods>>> FilterAsync(Func<Foods, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Foods>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Foods>>() { Success = false, Exception = e };
            }
        }

        #endregion

    }
}
