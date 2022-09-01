using AthenicConsulting.Core.Contracts;
using System.Linq.Expressions;

namespace AthenicConsulting.Core.Core.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        TEntity GetById<TEntity>(int id) where TEntity : BaseEntity;
        IEnumerable<TEntity> GetAll<TEntity>(int count = 25) where TEntity : BaseEntity;
        TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : BaseEntity;
        Task<TEntity> AddItem<TEntity>(TEntity item) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> AddItems<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity;
        IEnumerable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") where TEntity : BaseEntity;
        TEntity SoftDeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        TEntity HardDeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        TEntity UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        int Count<TEntity>() where TEntity : BaseEntity;
    }
}
