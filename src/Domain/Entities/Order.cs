namespace Domain.Entities
{
	public class Order
	{
	    public Address DeliveryAddress { get; set; }
    
	    public Cart Cart { get; set; }
	}
}
