using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Services
{
	public interface IRepository<TEntity> where TEntity : class
	{
		TEntity Get(int id);
		IQueryable<TEntity> Get();
		IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
		
		void Insert(TEntity entity);
		void Update(TEntity entity);
		
		void Remove(TEntity entity);

	}
}
