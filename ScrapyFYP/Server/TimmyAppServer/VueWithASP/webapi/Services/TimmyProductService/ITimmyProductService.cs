using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.ProductData;

namespace webapi.Services.TimmyProductService
{
	public interface ITimmyProductService
	{
		Task<TimmyProduct> AddTimmyProduct(AddTimmyProductDTO addTimmyProductDTO);
		Task<TimmyProduct> DeleteTimmyProduct(TimmyProductEssentialDTO timmyProductEssentialDTO);
		Task<TimmyProductData> GetAllAdoptedTimmyProductDict();
		Task<TimmyProductData> GetAllUnAdoptedTimmyProductDict();
		Task<List<string>> GetAllUnAdoptedTimmyProductName();
		Task<Boolean> AdoptTimmyProduct(TimmyProductEssentialDTO timmyProductEssentialDTO);
		Task<CategoryListAndRespondingBrandListDTO> GetCategoryBrandList();
		Task<List<TimmyProduct>> GetAllAdoptedProductList();
		Task<TimmyProduct> GetTimmyProductByName(string fullName);
		Task<List<string>> GetBrandList(string category);
		Task<List<string>> GetCategoryList();
	}
}
