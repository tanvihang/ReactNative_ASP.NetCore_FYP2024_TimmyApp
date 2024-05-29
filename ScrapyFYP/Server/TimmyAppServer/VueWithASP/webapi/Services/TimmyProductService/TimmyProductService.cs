using webapi.DAO.TimmyProductDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.ProductData;
using webapi.Utilities;

namespace webapi.Services.TimmyProductService
{
	public class TimmyProductService : ITimmyProductService
	{
		ITimmyProductDAO _timmyProductDAO;
        public TimmyProductService(ITimmyProductDAO timmyProductDAO)
        {
            _timmyProductDAO = timmyProductDAO;
        }

        public async Task<TimmyProduct> AddTimmyProduct(AddTimmyProductDTO addTimmyProductDTO)
		{
			if (string.IsNullOrEmpty(addTimmyProductDTO.subModel) || string.IsNullOrEmpty(addTimmyProductDTO.category) || string.IsNullOrEmpty(addTimmyProductDTO.brand) || string.IsNullOrEmpty(addTimmyProductDTO.model)) 
			{
				return null;
			}

			TimmyProduct tp = new TimmyProduct
			{
				TimmyProductFullName = GetFullName(addTimmyProductDTO.category!, addTimmyProductDTO.brand!, addTimmyProductDTO.model!),
				TimmyProductCategory = addTimmyProductDTO.category!,
				TimmyProductBrand = addTimmyProductDTO.brand!,
				TimmyProductModel = addTimmyProductDTO.model!,
				TimmyProductSubModel = addTimmyProductDTO.subModel!,
				TimmyProductAdopted = addTimmyProductDTO.adopt!
			};

			bool isAdded = await _timmyProductDAO.AddTimmyProduct(tp);
			if(isAdded)
			{
				return tp;
			}
			else
			{
				return null;
			}

		}

		public async Task<TimmyProduct> DeleteTimmyProduct(TimmyProductEssentialDTO timmyProductEssentialDTO)
		{
			string fullname = GetFullName(timmyProductEssentialDTO.category!, timmyProductEssentialDTO.brand!, timmyProductEssentialDTO.model!);

			TimmyProduct tp = await _timmyProductDAO.RemoveTimmyProduct(fullname);

			return tp;
		}

		public async Task<TimmyProductData> GetAllAdoptedTimmyProductDict()
		{
			try { 
				List<TimmyProduct> timmyProductList = await _timmyProductDAO.GetAllAdoptedTimmyProduct();

				TimmyProductData tpd = ConvertListToTimmyProductData(timmyProductList);
				return tpd;
			}
			catch
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("TimmyProductService", "GetAllTimmyProductDict", "Error creating dict"));
			}

		}

		public async Task<TimmyProductData> GetAllUnAdoptedTimmyProductDict()
		{
			try
			{
				List<TimmyProduct> timmyProductList = await _timmyProductDAO.GetAllUnAdoptedTimmyProduct();

				TimmyProductData tpd = ConvertListToTimmyProductData(timmyProductList);
				return tpd;
			}
			catch
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("TimmyProductService", "GetAllTimmyProductDict", "Error creating dict"));
			}
		}

		public string GetFullName(string category, string brand, string model)
		{
			string fullname = category + " " + brand + " " + model;
			return fullname;
		}

		public Task<List<string>> GetModelList(string category, string brand)
		{
			throw new NotImplementedException();
		}

		public TimmyProductData ConvertListToTimmyProductData(List<TimmyProduct> timmyProductList)
		{
			TimmyProductData timmyProductData = new TimmyProductData();

			foreach (TimmyProduct product in timmyProductList)
			{
				// Create Category
				if (!timmyProductData.Products.ContainsKey(product.TimmyProductCategory!))
				{
					timmyProductData.Products[product.TimmyProductCategory!] = new Dictionary<string, IDictionary<string, List<string>>>();
				}

				// Create Brand
				if (!timmyProductData.Products[product.TimmyProductCategory!].ContainsKey(product.TimmyProductBrand!))
				{
					timmyProductData.Products[product.TimmyProductCategory!][product.TimmyProductBrand!] = new Dictionary<string, List<string>>();
				}

				// Create SubModel
				if (!timmyProductData.Products[product.TimmyProductCategory!][product.TimmyProductBrand!].ContainsKey(product.TimmyProductSubModel!))
				{
					timmyProductData.Products[product.TimmyProductCategory!][product.TimmyProductBrand!][product.TimmyProductSubModel!] = new List<string>();
				}

				timmyProductData.Products[product.TimmyProductCategory!][product.TimmyProductBrand!][product.TimmyProductSubModel!].Add(product.TimmyProductModel!);
			}


			timmyProductData.Count = timmyProductList.Count;

			return timmyProductData;
		}

		public async Task<bool> AdoptTimmyProduct(TimmyProductEssentialDTO timmyProductEssentialDTO)
		{
				string productFullName = GetFullName(timmyProductEssentialDTO.category!, timmyProductEssentialDTO.brand!, timmyProductEssentialDTO.model!);
				bool isAdopted = await _timmyProductDAO.AdoptTimmyProduct(productFullName);

				return isAdopted;
		}

		public async Task<CategoryListAndRespondingBrandListDTO> GetCategoryBrandList()
		{
			List<CategoryBrandDTO> categoriesBrand = await _timmyProductDAO.GetCategoryBrandList();
			CategoryListAndRespondingBrandListDTO categoryListAndRespondingBrandList= new CategoryListAndRespondingBrandListDTO();

			// 1. Get all category first
			List<string> categories = await _timmyProductDAO.GetCategoryList();
			categoryListAndRespondingBrandList.categories = categories;

            await Console.Out.WriteLineAsync(categories[0]);

			categoryListAndRespondingBrandList.categoryBrands = new Dictionary<string, List<string>>();

            // 2. For every category, get responding brand
            foreach (string category in categories)
			{
				List<string> brand = await _timmyProductDAO.GetBrandList(category);
                await Console.Out.WriteLineAsync(brand[0]);
                categoryListAndRespondingBrandList.categoryBrands.Add(category, brand);
			
			}

			return categoryListAndRespondingBrandList;

		}

		public async Task<List<TimmyProduct>> GetAllAdoptedProductList()
		{
			List<TimmyProduct> list = await _timmyProductDAO.GetAllAdoptedTimmyProduct();

			return list;
		}

		public async Task<TimmyProduct> GetTimmyProductByName(string fullName)
		{
			return await _timmyProductDAO.GetTimmyProductByName(fullName);

		}

		public async Task<List<string>> GetBrandList(string category)
		{
			List<string> strings = await _timmyProductDAO.GetBrandList(category);

			return strings;
		}

		public async Task<List<string>> GetCategoryList()
		{
			List<string> strings = await _timmyProductDAO.GetCategoryList();
		
			return strings;
		}

		public async Task<List<string>> GetAllUnAdoptedTimmyProductName()
		{
			List<string> listOfName = await _timmyProductDAO.GetAllUnAdoptedTimmyProductName();
			return listOfName;
		}

		public async Task<PageEntity<TimmyProduct>> GetUnAdoptedTimmyProductPagination(PageDTO pageDTO)
		{
			return await _timmyProductDAO.GetUnAdoptedPagination(pageDTO);
		}

		public async Task<PageEntity<TimmyProduct>> GetAdoptedTimmyProductPagination(PageDTO pageDTO, string category, string brand)
		{
			return await _timmyProductDAO.GetAdoptedPagination(pageDTO, category, brand);
		}
	}
}
