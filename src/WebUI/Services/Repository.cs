using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Services;
using WebUI.Models;

namespace WebUI.Services
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected DbSet<TEntity> _dbSet;
		protected ShopContext _dbContext;

		public Repository(ShopContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = dbContext.Set<TEntity>();
		}

		public TEntity Get(int id)
		{
			return _dbSet.Find(id);
		}

		public IQueryable<TEntity> Get()
		{
			return _dbSet;
		}

		public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Where(predicate);
		}
		

		public void Insert(TEntity entity)
		{
			_dbSet.Add(entity);
		}
		
		public void Update(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
		}
		
		public void Remove(TEntity entity)
		{
			_dbSet.Remove(entity);
		}
	}
}
