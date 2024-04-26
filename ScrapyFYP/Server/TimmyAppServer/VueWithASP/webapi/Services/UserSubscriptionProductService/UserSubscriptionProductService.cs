using System.Collections.Generic;
using webapi.DAO.UserSubscriptionProductDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Services.UserSubscriptionService;
using webapi.Utilities;

namespace webapi.Services.UserSubscriptionProductService
{
	public class UserSubscriptionProductService : IUserSubscriptionProductService
	{
		private readonly IUserSubscriptionProductDAO _userSubscriptionProductDAO;
		private readonly IUserSubscriptionService _userSubscriptionService;
        public UserSubscriptionProductService(IUserSubscriptionProductDAO userSubscriptionProductDAO)
        {
            _userSubscriptionProductDAO = userSubscriptionProductDAO;
        }

		public async Task<UserSubscriptionProduct> AddUserSubscriptionProduct(AddUserSubscriptionProductDTO addUserSubscriptionProductDTO)
		{
			bool flag = false;

			// 1.获取订阅商品数量
			int count = await _userSubscriptionProductDAO.GetUserSubscriptionProductCount(addUserSubscriptionProductDTO.userSubscriptionId!);

            // 2.若大于，进行删除
            if (count >= 5)
			{
                await Console.Out.WriteLineAsync("Product more than 4");
                List<UserSubscriptionProduct> list = await _userSubscriptionProductDAO.GetUserSubscriptionProducts(addUserSubscriptionProductDTO.userSubscriptionId!);

				foreach (UserSubscriptionProduct userSubscriptionProduct in list)
				{
					if (userSubscriptionProduct.UserSubscriptionProductUserPreference == 0)
					{
						UserSubscriptionProduct usp = await _userSubscriptionProductDAO.RemoveUserSubscriptionProduct(userSubscriptionProduct.UserSubscriptionProductId!);
                        await Console.Out.WriteLineAsync("Removed one not wanted product");
                        flag = true;
						break;
					}
				}
			}
			else
			{
				flag = true;
			}

			if(flag == true)
			{

				UserSubscriptionProduct product = new UserSubscriptionProduct
				{
					UserSubscriptionProductId = StaticGenerator.GenerateId("USP_"),
					UserSubscriptionId = addUserSubscriptionProductDTO.userSubscriptionId!,
					UserSubscriptionProductPrice = addUserSubscriptionProductDTO.productPrice,
					UserSubscriptionProductPriceCny = addUserSubscriptionProductDTO.productPriceCNY,
					UserSubscriptionProductDescription = addUserSubscriptionProductDTO.productDescription,
					UserSubscriptionProductCondition = addUserSubscriptionProductDTO.productCondition,
					UserSubscriptionProductSpider = addUserSubscriptionProductDTO.productSpider,
					UserSubscriptionProductCurrency = addUserSubscriptionProductDTO.productCurrency,
					UserSubscriptionProductAddedDate = DateTime.Now,
					UserSubscriptionProductUserPreference = addUserSubscriptionProductDTO.productUserPreference,
					UserSubscriptionProductUrl = addUserSubscriptionProductDTO.productURL,
					UserSubscriptionProductImage = addUserSubscriptionProductDTO.productImage,
					UserSubscriptionProductUniqueId = addUserSubscriptionProductDTO.productUniqueId,
					UserSubscriptionProductTitle = addUserSubscriptionProductDTO.productTitle
				};

				bool isAdded = await _userSubscriptionProductDAO.AddUserSubscriptionProduct(product);

				if(isAdded == true)
				{
					return product;
				}

				// Product not added becuase it's same
				return null;
			}
			// Product exceed 5, cant add new product
			else
			{
				return null;
			}

		}

		public async Task<List<UserSubscriptionProduct>> GetUserSubscriptionProductsByUserSubscriptionId(string userSubscriptionId)
		{
			List<UserSubscriptionProduct> list = await _userSubscriptionProductDAO.GetUserSubscriptionProductsByUserSubscriptionId(userSubscriptionId);

			return list;
		}

		public async Task<UserSubscriptionProduct> RemoveUserSubscriptionProduct(string userSubscriptionProductId)
		{
			UserSubscriptionProduct usp = await _userSubscriptionProductDAO.RemoveUserSubscriptionProduct(userSubscriptionProductId);
			return usp;
		}
	}
}
