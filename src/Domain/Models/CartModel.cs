using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
	public class CartModel
	{
		ICartService _cartService;
		IUnitOfWork _unitOfWork;

		public CartModel(ICartService cartService, IUnitOfWork unitOfWork)
		{
			_cartService = cartService;
			_unitOfWork = unitOfWork;
		}

		public void Add(int productID, int quantity)
		{
			var cart = _cartService.Get();
			var product = _unitOfWork.Products.Get(productID);

			cart.AddLine(product, quantity);
			_cartService.Update(cart);
		}

		public void Remove(int productID)
		{
			throw new NotImplementedException();
		}


	    public Order MakeOrder()
	    {
	        throw new NotImplementedException();
	    }
	}
}
