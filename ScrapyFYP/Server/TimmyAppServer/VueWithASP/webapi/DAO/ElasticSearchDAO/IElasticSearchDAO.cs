using webapi.Models;
using webapi.Models.DTO;

namespace webapi.DAO.ElasticSearchDAO
{
	public interface IElasticSearchDAO
	{
		Task<List<ElasticProductDTO>> GetPaginationElasticProduct(ProductSearchTermDTO searchTermDTO, PageDTO pageDTO);
		Task<List<ElasticProductDTO>> GetLowPriceElasticProduct(ProductSearchTermDTO searchTermDTO);
		Task<PageEntity<ElasticProductDTO>> GetRandom10Product(PageDTO pageDTO, string category);
		Task<List<ElasticProductDTO>> GetLowPriceProductForUserSubscribe(UserSubscription userSubscription);
		Task<List<ElasticProductDTO>> GetLowPriceProductForPriceHistory(ProductSearchTermDTO productSearchTermDTO);
		Task<List<ElasticProductDTO>> GetUserFavourite(List<string> productUniqueId);
		Task<ElasticProductDTO> GetOneLowestPriceProduct(ProductSearchTermDTO productSearchTermDTO);
		Task<List<ElasticProductDTO>> GetProductOver7Days();
		Task<bool> UpdateProduct(ElasticProductDTO elasticProduct);
		Task<bool> DeleteAllProduct();
		Task<bool> RemoveOneProduct(string unique_id);
	}
}
