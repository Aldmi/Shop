using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class TestCart
	{
		[TestMethod]
		public void TestAddLines()
		{
			// Arrange
			var cart = new Cart();
			var product1 = new Product { Id = 1, UnitPrice = 100.0m };
			var product2 = new Product { Id = 2, UnitPrice = 150.5m };
			
			// Act
			cart.AddLine(product1, 2);
			cart.AddLine(product2, 3);

			// Assert
			Assert.AreEqual(100.0m * 2 + 150.5m * 3, cart.GetTotalAmount());			
		}

         
		// TODO: other tests 1
	}
}
