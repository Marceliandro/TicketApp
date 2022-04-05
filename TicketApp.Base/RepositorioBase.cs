using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketApp.Base.Interfaces;

namespace TicketApp.Base
{
    public class RepositorioBase<TEntity, TId> : IRepositorioBase<TEntity, TId> where TId : struct where TEntity : Entity<TEntity, TId>
    {
        protected BaseContext Db { get; private set; }
        protected DbSet<TEntity> DbSet { get; }

        public RepositorioBase(BaseContext context)
        {
            Db = context ?? throw new ArgumentException("Falha ao inicializar o repositório. Contexto inválido.");
            DbSet = Db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get => DbSet;
        public bool Commit() => Db.Commit();
        public Task<bool> CommitAsync() => Db.CommitAsync();
        public void Delete(TEntity entity) => DbSet.Remove(entity);
        public void Disposes() => Db.Dispose();
        public virtual TEntity GetById(TId id) => DbSet.Find(id);
        public virtual ValueTask<TEntity> GetByIdAsync(TId id) => DbSet.FindAsync(id);
        public virtual TEntity Find(Expression<Func<TEntity, bool>> expression) => DbSet.FirstOrDefault(expression);

        public virtual void Add(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Added;
            DbSet.Add(entity);
        }

        public virtual TEntity Add(TEntity entity, bool commit)
        {
            Db.Entry(entity).State = EntityState.Added;
            DbSet.Add(entity);

            if (commit)
                Db.Commit();

            return entity;
        }

        public virtual ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Added;
            return Db.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
        }
    }
}
