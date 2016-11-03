using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HomeBudgetViewer.Database.Engine.Entities;

namespace HomeBudgetViewer.Database.Engine.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}