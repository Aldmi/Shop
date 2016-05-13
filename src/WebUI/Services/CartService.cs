using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain;
using Domain.Services;

namespace WebUI.Services
{
	public class CartService : ICartService
	{
		const string CartKey = "cart";

		public Cart Get()
		{
			if (HttpContext.Current.Session[CartKey] == null)
			{
				HttpContext.Current.Session[CartKey] = new Cart();
			}

			return (Cart)HttpContext.Current.Session[CartKey];
		}

		public void Update(Cart cart)
		{
			HttpContext.Current.Session[CartKey] = cart;
		}
	}
}
