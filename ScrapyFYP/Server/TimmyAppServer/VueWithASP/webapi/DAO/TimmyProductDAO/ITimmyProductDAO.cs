using webapi.Models;
using webapi.Models.DTO;

namespace webapi.DAO.TimmyProductDAO
{
	public interface ITimmyProductDAO
	{
		Task<bool> AddTimmyProduct(TimmyProduct timmyProduct);
		Task<TimmyProduct> RemoveTimmyProduct(string productFullName);
		Task<List<CategoryBrandDTO>> GetCategoryBrandList();
		Task<List<TimmyProduct>> GetAllAdoptedTimmyProduct();
		Task<List<TimmyProduct>> GetAllUnAdoptedTimmyProduct();
		Task<PageEntity<TimmyProduct>> GetUnAdoptedPagination(PageDTO pageDTO);
		Task<PageEntity<TimmyProduct>> GetAdoptedPagination(PageDTO pageDTO, string category, string brand);
		Task<List<string>> GetAllUnAdoptedTimmyProductName();
		Task<bool> AdoptTimmyProduct(string productFullName);
		Task<List<TimmyProduct>> GetAllProductList();
		Task<TimmyProduct> GetTimmyProductByName(string fullName);
		Task<List<string>> GetBrandList(string category);
		Task<List<string>> GetCategoryList();
	}

}
