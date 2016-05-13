using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Domain;
using Domain.Services;
using WebUI.Models;

namespace WebUI.Services
{
	public class UnitOfWork : IUnitOfWork
	{
		private ShopContext _context;

		public UnitOfWork(ShopContext context)
		{
			_context = context;

			Products = new Repository<Product>(context);
		}

		public IRepository<Product> Products { get; private set; }

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
