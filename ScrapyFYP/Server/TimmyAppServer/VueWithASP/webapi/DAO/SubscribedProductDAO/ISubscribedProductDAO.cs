using webapi.Models;
using webapi.Models.DTO;

namespace webapi.DAO.SubscribedProductDAO
{
	public interface ISubscribedProductDAO
	{
		Task<SubscribedProduct> AddSubscribedProduct(AddSubscribedProductDTO addSubscribedProductDTO);
		Task<SubscribedProduct> RemoveSubscribedProduct(string productFullName);
		Task<List<SubscribedProduct>> GetAllSubscribedProducts();
		Task<List<SubscribedProduct>> GetLevelSubscribedProducts(int level);
	}
}
