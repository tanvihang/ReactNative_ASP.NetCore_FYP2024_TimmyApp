using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.DAO.TimmyProductDAO
{
	public class TimmyProductDAO : ITimmyProductDAO
	{

		TimmyDbContext _context;

        public TimmyProductDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }

        public async Task<bool> AddTimmyProduct(TimmyProduct timmyProduct)
		{
			try
			{
				await _context.AddAsync(timmyProduct);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "AddTimmyProduct", $"Product already exist {timmyProduct.TimmyProductFullName}"));
			}
		}

		public async Task<bool> AdoptTimmyProduct(string productFullName)
		{
			try
			{
				TimmyProduct tp = await _context.TimmyProducts.FirstOrDefaultAsync(tp => tp.TimmyProductFullName == productFullName);

				if(tp == null)
				{
					return false;
				}

				if(tp.TimmyProductAdopted == 1)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "AdoptTimmyProduct", "Product is already adopted, consider delete"));
				}
				else 
				{
					tp.TimmyProductAdopted = 1;
					await _context.SaveChangesAsync();
					return true;

				}

			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "AdoptTimmyProduct", ex.Message));
			}
		}

		public async Task<List<TimmyProduct>> GetAllAdoptedTimmyProduct()
		{
			List<TimmyProduct> tpl = await _context.TimmyProducts.Where(tp => tp.TimmyProductAdopted == 1).ToListAsync();
			return tpl;
		}

		public async Task<List<TimmyProduct>> GetAllProductList()
		{
			try
			{
				List<TimmyProduct> timmyProducts = await _context.TimmyProducts.ToListAsync();

				return timmyProducts;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "GetAllProductList", ex.Message));
			}
		}

		public async Task<List<TimmyProduct>> GetAllUnAdoptedTimmyProduct()
		{
			List<TimmyProduct> tpl = await _context.TimmyProducts.Where(tp => tp.TimmyProductAdopted == 0).ToListAsync();
			return tpl;
		}

		public async Task<List<string>> GetBrandList(string category)
		{
			try
			{
				var productBrandList = await _context.TimmyProducts.Where(tp => tp.TimmyProductAdopted == 1 && tp.TimmyProductCategory == category).Select(p => p.TimmyProductBrand).Distinct().ToListAsync();
				return productBrandList;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "GetBrandList", ex.Message));

			}

		}

		public async Task<List<CategoryBrandDTO>> GetCategoryBrandList()
		{
			try
			{
				var productCategoryList = await _context.TimmyProducts.Where(tp => tp.TimmyProductAdopted == 1).Select(p => new CategoryBrandDTO
				{
					brand = p.TimmyProductBrand,
					category = p.TimmyProductCategory
				}).Distinct().ToListAsync();

                return productCategoryList;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "GetCategoryList", ex.Message));
			}
		}

		public async Task<List<string>> GetCategoryList()
		{

			try
			{
				var productCategoryList = await _context.TimmyProducts.Where(tp => tp.TimmyProductAdopted == 1).Select(p => p.TimmyProductCategory).Distinct().ToListAsync();
				return productCategoryList;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "GetCategoryList", ex.Message));

			}
		}

		public async Task<TimmyProduct> GetTimmyProductByName(string fullName)
		{
			try
			{
				TimmyProduct product = await _context.TimmyProducts.FirstOrDefaultAsync(tp => tp.TimmyProductFullName == fullName);

				return product;
				
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "GetTimmyProductByName", ex.Message));

			}
		}

		public async Task<TimmyProduct> RemoveTimmyProduct(string productFullName)
		{
			TimmyProduct tp = await _context.TimmyProducts.FirstOrDefaultAsync(tp => tp.TimmyProductFullName == productFullName)!;

			if (tp != null)
			{
				try
				{
					_context.TimmyProducts.Remove(tp);
					await _context.SaveChangesAsync();
					return tp;
				}
				catch
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "RemoveTimmyProduct", "Failed To Save Change"));
				}
			}
			//throw new Exception(StaticGenerator.GenerateDTOErrorMessage("TimmyProductDAO", "RemoveTimmyProduct", "Invalid Product Full Name"));
			return null;
		}
	}
}
