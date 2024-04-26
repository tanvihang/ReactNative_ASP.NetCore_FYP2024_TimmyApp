using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.DAO.SubscribedProductDAO
{
	public class SubscribedProductDAO : ISubscribedProductDAO
	{
		TimmyDbContext _context;
        public SubscribedProductDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }
        public async Task<SubscribedProduct> AddSubscribedProduct(AddSubscribedProductDTO addSubscribedProductDTO)
		{
			try
			{
				SubscribedProduct existSubscribedProduct = await _context.SubscribedProducts.FirstOrDefaultAsync(sp => sp.SubscribedProductFullName == addSubscribedProductDTO.fullName!);
				
				// 若为空直接添加进去
				if (existSubscribedProduct == null)
				{
					SubscribedProduct product = new SubscribedProduct
					{
						SubscribedProductFullName = addSubscribedProductDTO.fullName,
						SubscribedProductCategory = addSubscribedProductDTO.category,
						SubscribedProductBrand = addSubscribedProductDTO.brand,
						SubscribedProductModel = addSubscribedProductDTO.model,
						SubscribedProductHighestLevel = addSubscribedProductDTO.userLevel,
						SubscribedProductCount = 1
					};

					await _context.SubscribedProducts.AddAsync(product);
					await _context.SaveChangesAsync();
					return product;
				}
				// 已经有相同的订阅商品
				else
				{
					existSubscribedProduct.SubscribedProductCount = existSubscribedProduct.SubscribedProductCount + 1;

					//并且比较Userlevel
					if(existSubscribedProduct.SubscribedProductHighestLevel < addSubscribedProductDTO.userLevel)
					{
						existSubscribedProduct.SubscribedProductHighestLevel = addSubscribedProductDTO.userLevel;
					}

					await _context.SaveChangesAsync();
					return existSubscribedProduct;
				}
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("SubscribedProductDAO", "AddSubscribedProduct", ex.Message));
			}
		}

		public async Task<List<SubscribedProduct>> GetAllSubscribedProducts()
		{
			try
			{
				List<SubscribedProduct> list = await _context.SubscribedProducts.ToListAsync();
				return list;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("SubscribedProductDAO", "AddSubscribedProduct", ex.Message));
			}
		}

		public async Task<List<SubscribedProduct>> GetLevelSubscribedProducts(int level)
		{
			try
			{
				List<SubscribedProduct> list = _context.SubscribedProducts.Where(sp => sp.SubscribedProductHighestLevel == level).ToList();
			
				if(list == null)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("SubscribedProductDAO", "GetLevelSubscribedProducts", $"No {level} level subscribed product"));
				}

				return list;

			}
			catch
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("SubscribedProductDAO", "GetLevelSubscribedProducts", "Error reading SubscribedProducts"));
			}

		}

		public async Task<SubscribedProduct> RemoveSubscribedProduct(string productFullName)
		{
			try
			{
				SubscribedProduct subscribedProduct = await _context.SubscribedProducts.FirstOrDefaultAsync(sp => sp.SubscribedProductFullName == productFullName);

				if (subscribedProduct != null)
				{
					if(subscribedProduct.SubscribedProductCount == 1)
					{
						subscribedProduct.SubscribedProductCount = subscribedProduct.SubscribedProductCount - 1;
						_context.Remove(subscribedProduct);
					}
					else
					{
						subscribedProduct.SubscribedProductCount = subscribedProduct.SubscribedProductCount - 1;
					}

					await _context.SaveChangesAsync();
					return subscribedProduct;
				}
				else
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("SubscribedProductDAO", "RemoveSubscribedProduct", $"No product {productFullName}"));
				}
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("SubscribedProductDAO", "RemoveSubscribedProduct", ex.Message));
			}
		}
	}
}
