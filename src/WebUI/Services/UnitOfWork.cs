﻿using System;
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
            Orders= new Repository<Order>(context);
        }

		public IRepository<Product> Products { get; }

	    public IRepository<Order> Orders { get; }

        public async Task<int> SaveAsync()
		{
             return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
