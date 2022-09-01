using AthenicConsulting.Core.Contracts;
using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AthenicConsulting.Core.Core.Models
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AthenicConsultingContext _context;
        public GenericRepository(AthenicConsultingContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddItem<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            var result = await _context.AddAsync(item);
            return result.Entity;
        }

        public IEnumerable<TEntity> GetAll<TEntity>(int count = 25) where TEntity : BaseEntity
        {
            var query = _context.Set<TEntity>().AsQueryable().Take(count);
            return query.ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity
        {
            var query = await _context.Set<TEntity>().AsQueryable().ToListAsync();
            return query;
        }

        public IEnumerable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : BaseEntity
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetById<TEntity>(int id) where TEntity : BaseEntity
        {
            var query = _context.Set<TEntity>().AsQueryable();
            var result = query.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : BaseEntity
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (filter == null) { return query.FirstOrDefault(); }
            return query.FirstOrDefault(filter);
        }

        public TEntity UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var updatedEntity = _context.Set<TEntity>().Update(entity);
            return updatedEntity.Entity;
        }

        public TEntity SoftDeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity.Deactivatated = true;
            entity.DeactivatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;
            var updatedEntity = _context.Set<TEntity>().Update(entity);
            return updatedEntity.Entity;
        }

        public TEntity HardDeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
           var removedEntity = _context.Set<TEntity>().Remove(entity);
            return removedEntity.Entity;
        }

        public async Task<IEnumerable<TEntity>> AddItems<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            var dbSet = _context.Set<TEntity>();
            await dbSet.AddRangeAsync(entities);
            return entities;
        }

        public int Count<TEntity>() where TEntity : BaseEntity
        {
            var dbSet = _context.Set<TEntity>();
            return dbSet.Count();
        }
    }
}
