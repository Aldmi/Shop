using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Cart
	{
		private List<Line> _lines = new List<Line>();

		public IEnumerable<Line> Lines { get { return _lines; }	}

		public void AddLine(Product product, int quantity)
		{
			var existingLine = _lines
				.Where(line => line.Product.Id == product.Id)
				.FirstOrDefault();

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
