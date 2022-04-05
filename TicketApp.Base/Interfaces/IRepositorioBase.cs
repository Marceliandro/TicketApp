using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TicketApp.Base.Interfaces
{
    public interface IRepositorioBase<TEntity, TId> where TEntity : Entity<TEntity, TId> where TId : struct
    {
        void Add(TEntity entity);
        TEntity Add(TEntity entity, bool commit);
        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity);
        void Disposes();
        TEntity GetById(TId id);
        TEntity Find(Expression<Func<TEntity, bool>> expression);
        ValueTask<TEntity> GetByIdAsync(TId id);
        bool Commit();
        Task<bool> CommitAsync();
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Get { get; }
    }
}
