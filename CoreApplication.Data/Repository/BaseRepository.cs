using CoreApplication.Data.Contracts;
using CoreApplication.Data.Contracts.Context;
using CoreApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace CoreApplication.Data
{
    public abstract class BaseRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        internal IServiceProvider _serviceProvider;

        public BaseRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet = context.Set<TEntity>();


                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
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
        }

        public virtual TEntity GetByID(object id)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet = context.Set<TEntity>();
                return dbSet.Find(id);
            }
        }

        public virtual void Insert(TEntity entity)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                entity.CreationDate = DateTime.Now;
                entity.LastModifydate = DateTime.Now;

                dbSet.Add(entity);
                context.SaveChanges();
            }
        }

        public virtual void Delete(object id)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                TEntity entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                entityToUpdate.LastModifydate = DateTime.Now;

                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
