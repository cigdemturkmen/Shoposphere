using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shoposphere.Data;
using Shoposphere.Data.Entities.Interfaces;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Shoposphere.Services.Concrete
{
    public class EFRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly ShoposphereDbContext _context;
        public EFRepository(ShoposphereDbContext context)
        {
            _context = context;
        }

        public bool Add(T entity) // returns bool
        {
            entity.IsActive = true;
            entity.CreatedDate = DateTime.Now;

            _context.Set<T>().Add(entity);

            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = Get(x => x.Id == id && x.IsActive);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;

            return _context.SaveChanges() > 0;

        }

        public bool Edit(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;

            _context.Set<T>().Update(entity);

            return _context.SaveChanges() > 0;
        }

        public T Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            return query.FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);

            }

            return query.ToList();
        }
    }
}
