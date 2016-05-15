using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using WebUI.Models;

namespace WebUI.Services
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ShopContext _context;

		public UnitOfWork(ShopContext context)
		{
			_context = context;

			Products = new Repository<Product>(context);
		}

		public IRepository<Product> Products { get; private set; }

		public async Task<int> Save()
		{
             return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
