using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
	{
		IRepository<Product> Products { get; }

        IRepository<Order> Orders { get; }

        // TODO: add reps

        Task<int> SaveAsync();
	}
}
