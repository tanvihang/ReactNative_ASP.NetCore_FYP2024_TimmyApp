using webapi.DAO.SubscribedProductDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.Services.SubscribedProductService
{
	public class SubscribedProductService : ISubscribedProductService
	{
		ISubscribedProductDAO _subscribedProductDAO;
        public SubscribedProductService(ISubscribedProductDAO subscribedProductDAO)
        {
            _subscribedProductDAO	= subscribedProductDAO;
        }
        public async Task<SubscribedProduct> AddSubscribedProduct(UpdateSubscribedProductDTO updateSubscribedProductDTO)
		{
			try
			{
				string fullProductName = GetFullProductName(updateSubscribedProductDTO);

				SubscribedProduct product =  await _subscribedProductDAO.AddSubscribedProduct(new AddSubscribedProductDTO
				{
					fullName = fullProductName,
					category = updateSubscribedProductDTO.category,
					brand = updateSubscribedProductDTO.brand,
					model = updateSubscribedProductDTO.model,
					userLevel = updateSubscribedProductDTO.user_level
				});

				return product;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("SubscribedProductService", "AddSubscribedProduct", ex.Message));
			}
		}
		public async Task<SubscribedProduct> RemoveSubscribedProduct(UpdateSubscribedProductDTO updateSubscribedProductDTO)
		{

			string fullName = GetFullProductName(updateSubscribedProductDTO);

			SubscribedProduct subscribedProduct = await _subscribedProductDAO.RemoveSubscribedProduct(fullName);

			return subscribedProduct;
		}

		public async Task<List<SubscribedProduct>> GetAllSubscribedProduct()
		{

			List<SubscribedProduct> list = await _subscribedProductDAO.GetAllSubscribedProducts();

			if(list.Count == 0)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("SubscribedProductService", "GetAllSubscribedProduct", "No Subscribed Product"));
			}

			return list;
		}


		string GetFullProductName(UpdateSubscribedProductDTO updateSubscribedProductDTO)
		{
			string fullname = updateSubscribedProductDTO.category + " " + updateSubscribedProductDTO.brand + " " + updateSubscribedProductDTO.model;

			return fullname;
		}

		public async Task<List<SubscribedProduct>> GetLevelSubscribedProducts(int level)
		{
			return await _subscribedProductDAO.GetLevelSubscribedProducts(level);

		}
	}
}
