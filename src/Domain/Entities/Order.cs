using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class Order
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public Address DeliveryAddress { get; set; }

        [Required]
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
