using Microsoft.EntityFrameworkCore;
using Recodme.ShokuDex.Data.Base;
using Recodme.ShokuDex.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.ShokuDex.DataAccess.DataAccessObjects
{
    public class BaseDataAccessObject<T> where T : Entity
    {
        private FoodLogContext _context;
        public BaseDataAccessObject()
        {
            _context = new FoodLogContext();
        }

        #region Create
        public void Create(T item)
        {
            _context.Set<T>.Add(item);
            _context.SaveChanges();
        }

        public async Task CreateAsync(T item)
        {
            await _context.Set<T>.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public T Read(Guid id)
        {
            return _context.Set<T>.FirstOrDefault(x => x.Id == id);
        }
        public async Task<T> ReadAsync(Guid id)
        {
            return await
                new Task<T>
                (
                    () => _context.Set<T>.FirstOrDefault(x => x.Id == id)
                );
        }
        #endregion

        #region Update
        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public async Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(T item)
        {
            item.IsDeleted = true;
            Update(item);
        }

        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }

        public async Task DeleteAsync(T item)
        {
            item.IsDeleted = true;
            await UpdateAsync(item);
        }
        public async Task DeleteAsync(Guid id)
        {
            var item = ReadAsync(id).Result;
            if (item == null) return;
            await DeleteAsync(item);
        }
        #endregion

        #region List
        public List<T> List()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        #endregion
    }
}