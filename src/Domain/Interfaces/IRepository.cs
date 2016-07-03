using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
		IQueryable<TEntity> Get();
		IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
		
		void Insert(TEntity entity);
		void Update(TEntity entity);		
		void Remove(TEntity entity);
	}
}
