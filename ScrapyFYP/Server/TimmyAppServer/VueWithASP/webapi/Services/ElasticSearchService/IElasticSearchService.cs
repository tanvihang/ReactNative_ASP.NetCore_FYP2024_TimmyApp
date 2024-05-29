using Nest;
using webapi.Models;
using webapi.Models.DTO;

namespace webapi.Services.ElasticSearchService
{
	public interface IElasticSearchService
	{
		Task<PageEntity<ElasticProductDTO>> SearchProduct(ProductSearchTermDTO searchTermDTO, PageDTO pageDTO, string? userId);
		Task<PageEntity<ElasticProductDTO>> GetRandom10Product(PageDTO pageDTO, string category);
		Task<List<ElasticProductDTO>> GetLowestPriceProduct(UserSubscription userSubscription);
		Task<List<ElasticProductDTO>> GetLowPriceProductForPriceHistory(ProductSearchTermDTO productSearchTermDTO);
		Task<bool> UpdateProduct(ElasticProductDTO elasticProduct);
		Task<List<ElasticProductDTO>> GetLowPriceProductForUserSubscribe(UserSubscription userSubscription);
		Task<List<ElasticProductDTO>> GetUserFavourite(List<string> productUniqueId);
		Task<ElasticProductDTO> GetOneLowestPriceProduct(ProductSearchTermDTO productSearchTermDTO);
		Task<List<ElasticProductDTO>> GetProductOver7Days();
		Task<bool> DeleteAllProduct();
		Task<bool> RemoveOneProduct(string unique_id);
		Task<List<ElasticCategoryCountDTO>> GetElasticProductCategoriesCount();
		Task<List<ElasticCategoryCountDTO>> GetElasticProductBrandCount(string category);
		Task<ElasticSearchModelDTO> GetElasticProductModelCount(string category, string model);



	}
}
