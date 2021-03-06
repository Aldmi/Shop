﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

        [Required(ErrorMessage = "Введите название продукта")]
		public string Name { get; set; }

        public decimal UnitPrice { get; set; } 

        public string PictureRef { get; set; }
	}
}
