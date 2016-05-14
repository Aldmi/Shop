using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain.Interfaces;
using WebUI.Models;

namespace WebUI.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected DbSet<TEntity> DbSet;
		protected ShopContext DbContext;

		public Repository(ShopContext dbContext)
		{
			DbContext = dbContext;
			DbSet = dbContext.Set<TEntity>();
		}

		public TEntity Get(int id)
		{
			return DbSet.Find(id);
		}

		public IQueryable<TEntity> Get()
		{
			return DbSet;
		}

		public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
		{
			return DbSet.Where(predicate);
		}
		
		public void Insert(TEntity entity)
		{
			DbSet.Add(entity);
		}
		
		public void Update(TEntity entity)
		{
			DbContext.Entry(entity).State = EntityState.Modified;
		}
		
		public void Remove(TEntity entity)
		{
			DbSet.Remove(entity);
		}
	}
}
