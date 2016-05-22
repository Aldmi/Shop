using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace UnitTests
{
	/// <summary>
	/// Summary description for TestCartController
	/// </summary>
	[TestClass]
	public class TestCartModel
	{
		[TestMethod]
		public void TestAdd()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Products.Get(It.IsAny<int>()))
                .Returns(
                    new Product
                    {
                        Id = 1,
                        UnitPrice = 111
                    }
            );

            var mockCartService = new Mock<ICartService>();
			var cart = new Cart();
			mockCartService.Setup(m => m.Get()).Returns(cart);

			var cartModel = new CartModel(mockCartService.Object, mockUnitOfWork.Object);
			cartModel.Add(1, 2);

			Assert.AreEqual(222, cart.GetTotalAmount());
		}


		// TODO tests for:
		// Remove
		// CreateOrder
		

	}
}
