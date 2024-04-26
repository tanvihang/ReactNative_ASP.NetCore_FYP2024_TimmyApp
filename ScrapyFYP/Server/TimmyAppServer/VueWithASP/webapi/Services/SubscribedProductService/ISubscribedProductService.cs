using webapi.Models;
using webapi.Models.DTO;

namespace webapi.Services.SubscribedProductService
{
	public interface ISubscribedProductService
	{
		Task<SubscribedProduct> AddSubscribedProduct(UpdateSubscribedProductDTO updateSubscribedProductDTO);
		Task<SubscribedProduct> RemoveSubscribedProduct(UpdateSubscribedProductDTO updateSubscribedProductDTO);
		Task<List<SubscribedProduct>> GetAllSubscribedProduct();

		Task<List<SubscribedProduct>> GetLevelSubscribedProducts(int level);
	}
}
