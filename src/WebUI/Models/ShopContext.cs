﻿using System.Data.Entity;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using WebUI.Security;

namespace WebUI.Models
{
    public class ShopContext : IdentityDbContext<ShopUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ShopContext() : base("name=WebUIContext")
        {
        }

		public DbSet<Product> Products { get; set; }
	}
}
