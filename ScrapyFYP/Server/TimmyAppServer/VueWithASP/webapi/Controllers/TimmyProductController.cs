using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.ProductData;
using webapi.Models.Response;
using webapi.Services.TimmyProductService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TimmyProductController : ControllerBase
	{
		ITimmyProductService _timmyProductService;
        
		public TimmyProductController(ITimmyProductService timmyProductService)
        {
            _timmyProductService = timmyProductService;
        }

        [HttpPost("AddTimmyProduct")]
		public async Task<ResponseData<TimmyProduct>> AddTimmyProduct(AddTimmyProductDTO dto)
		{
			try
			{
				TimmyProduct tp = await _timmyProductService.AddTimmyProduct(dto);

				if(tp != null)
				{
					return ResponseData<TimmyProduct>.Success(tp,$"Add TimmyProduct succeed: item model {dto.model}");
				}
				else
				{
					return ResponseData<TimmyProduct>.Failure($"Item alreadly exist in database or input error: {dto.model}");
				}

			}
			catch (Exception ex)
			{
				return ResponseData<TimmyProduct>.Failure(ex.Message);
			}
		}

		[HttpPost("DeleteTimmyProduct")]
		public async Task<ResponseData<TimmyProduct>> DeleteTimmyProduct(TimmyProductEssentialDTO timmyProductEssentialDTO)
		{
			try
			{
				TimmyProduct tp = await _timmyProductService.DeleteTimmyProduct(timmyProductEssentialDTO);

				if(tp != null )
				{
					return ResponseData<TimmyProduct>.Success(tp, $"Delete TimmyProduct succeed：{tp.TimmyProductFullName}");
				}
				else
				{
					return ResponseData<TimmyProduct>.Failure($"TimmyProduct not exist or wrong input：{timmyProductEssentialDTO.model}");
				}

			}
			catch(Exception ex)
			{
				return ResponseData<TimmyProduct>.Failure(ex.Message);
			}
		}

		[HttpGet("GetAllAdoptedTimmyProductDict")]
		public async Task<ResponseData<TimmyProductData>> GetAllAdoptedTimmyProductDict()
		{
			try
			{
				TimmyProductData timmyProductData = await _timmyProductService.GetAllAdoptedTimmyProductDict();

				return ResponseData<TimmyProductData>.Success(timmyProductData);
			}
			catch(Exception ex)
			{
				return ResponseData<TimmyProductData>.Failure(ex.Message);
			}
		}

		[HttpGet("GetAllUnAdoptedTimmyProductDict")]
		public async Task<ResponseData<TimmyProductData>> GetAllUnAdoptedTimmyProductDict()
		{
			try
			{
				TimmyProductData timmyProductData = await _timmyProductService.GetAllUnAdoptedTimmyProductDict();

				return ResponseData<TimmyProductData>.Success(timmyProductData);
			}
			catch (Exception ex)
			{
				return ResponseData<TimmyProductData>.Failure(ex.Message);
			}
		}

		[HttpPost("AdoptTimmyProduct")]
		public async Task<ResponseData<bool>> AdoptTimmyProduct(TimmyProductEssentialDTO timmyProductEssentialDTO)
		{
			try
			{
				bool isAdopted = await _timmyProductService.AdoptTimmyProduct(timmyProductEssentialDTO);

				if(isAdopted)
				{
					return ResponseData<bool>.Success(isAdopted,"Adopt Successfully");
				}
				else
				{
					return ResponseData<bool>.Failure("Adopt Unsuccessfully");
				}
			}
			catch (Exception ex)
			{
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

		[HttpGet("GetCategoryBrandList")]
		public async Task<ResponseData<CategoryListAndRespondingBrandListDTO>> GetCategoryBrandList()
		{
			try
			{
				CategoryListAndRespondingBrandListDTO list =  await _timmyProductService.GetCategoryBrandList();

				return ResponseData<CategoryListAndRespondingBrandListDTO>.Success(list);
			}
			catch(Exception ex)
			{
				return ResponseData<CategoryListAndRespondingBrandListDTO>.Failure(ex.Message);
			}
		}

		[HttpGet("GetUnAdoptedTimmyProductName")]
		public async Task<ResponseData<List<string>>> GetUnAdoptedTimmyProductName()
		{
			try
			{
				List<string> names = await _timmyProductService.GetAllUnAdoptedTimmyProductName();
				
				if(names.Count > 0)
				{
					return ResponseData<List<string>>.Success(names);
				}
				return ResponseData<List<string>>.Success("No UnAdopted Product");

			}
			catch(Exception ex) { 
				return ResponseData<List<string>>.Failure(ex.Message);
			}
		}

		[HttpPost("GetUnAdoptedTimmyProductPagination")]
		public async Task<ResponseData<PageEntity<TimmyProduct>>> GetUnAdoptedTimmyProductPagination(PageDTO pageDTO)
		{
			try
			{
				PageEntity<TimmyProduct> list = await _timmyProductService.GetUnAdoptedTimmyProductPagination(pageDTO);

				return ResponseData<PageEntity<TimmyProduct>>.Success(list);
			}
			catch(Exception ex)
			{
				return ResponseData<PageEntity<TimmyProduct>>.Failure(ex.Message);
			}
		}

		[HttpPost("GetAdoptedTimmyProductPagination")]
		public async Task<ResponseData<PageEntity<TimmyProduct>>> GetAdoptedTimmyProductPagination(PageDTO pageDTO, string category = "", string brand = "")
		{
			try
			{
				PageEntity<TimmyProduct> list = await _timmyProductService.GetAdoptedTimmyProductPagination(pageDTO, category, brand);

				return ResponseData<PageEntity<TimmyProduct>>.Success(list);
			}
			catch (Exception ex)
			{
				return ResponseData<PageEntity<TimmyProduct>>.Failure(ex.Message);
			}
		}
	}
}
