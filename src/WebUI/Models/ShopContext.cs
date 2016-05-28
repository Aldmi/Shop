using System.Data.Entity;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using WebUI.Security;

namespace WebUI.Models
{
    public class ShopContext : IdentityDbContext<ShopUser>
    {
        public ShopContext() : base("name=WebUIContext")
        {
        }

		public DbSet<Product> Products { get; set; }

       // public DbSet<Order> Orders { get; set; }
    }
}
