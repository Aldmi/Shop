using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
	public class Cart
	{
		private readonly List<Line> _lines = new List<Line>();

		public IEnumerable<Line> Lines => _lines;

	    public void AddLine(Product product, int quantity)
		{
			var existingLine = _lines
				.FirstOrDefault(line => line.Product.Id == product.Id);

			if (existingLine == null)
			{
				_lines.Add(new Line
				{
					Product = product,
					Quantity = quantity
				});
			}
			else
			{
				existingLine.Quantity += quantity;
			}
		}

		// TODO: remove line

		// TODO: clear all

	
		public decimal GetTotalAmount()
		{
			return _lines.Sum(line => line.Product.UnitPrice * line.Quantity);
		}
	}
}
