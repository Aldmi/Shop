using System.Web;
using Domain.Entities;
using Domain.Interfaces;

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
