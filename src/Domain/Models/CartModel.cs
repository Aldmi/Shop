﻿using System;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Models
{
    public class CartModel
	{
        readonly ICartService _cartService;
        readonly IUnitOfWork _unitOfWork;

		public CartModel(ICartService cartService, IUnitOfWork unitOfWork)
		{
			_cartService = cartService;
			_unitOfWork = unitOfWork;
		}

		public void Add(int productId, int quantity)
		{
			var cart = _cartService.Get();
			var product = _unitOfWork.Products.Get(productId);

			cart.AddLine(product, quantity);
			_cartService.Update(cart);
		}

		public void Remove(int productId)
		{
			throw new NotImplementedException();
		}


	    public Order MakeOrder()
	    {
	        throw new NotImplementedException();
	    }
	}
}
