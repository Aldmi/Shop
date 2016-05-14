using Domain.Entities;

namespace Domain.Interfaces
{
	public interface ICartService
	{
		Cart Get();
		void Update(Cart cart);
	}
}
