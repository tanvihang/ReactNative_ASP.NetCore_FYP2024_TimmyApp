using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Configuration;
using Nest;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.DAO.ElasticSearchDAO
{
	public class ElasticSearchDAO : IElasticSearchDAO
	{
		private readonly ElasticClient _client;
		private readonly IConfiguration _configuration;

		private readonly string _index;
		private readonly string _url;
		private readonly string _password;
		private readonly string _username;
		private readonly string[] _spiders;

		public ElasticSearchDAO(IConfiguration configuration)
		{
			_configuration = configuration;

			_url = _configuration["Elasticsearch:Url"];
			_username = _configuration["Elasticsearch:Username"];
			_password = _configuration["Elasticsearch:Password"];
			_index = _configuration["Elasticsearch:Index"];

			// read the spiders
			_spiders = _configuration.GetSection("Spiders").Get<string[]>()!;

			var settings = new ConnectionSettings(new Uri(_url)).DefaultIndex(_index).BasicAuthentication(_username, _password);
			_client = new ElasticClient(settings);
		}

		public async Task<bool> DeleteAllProduct()
		{
			try
			{
				var response = await _client.DeleteByQueryAsync<ElasticProductDTO>(d => d
					.Index(_index) // Specify the index from which you want to delete all items
					.Query(q => q.MatchAll()) // Match all documents for deletion
					);

				if (response.IsValid)
				{
					Console.WriteLine($"All items in index '{_index}' deleted successfully.");
					return true;
				}
				else
				{
					Console.WriteLine($"Failed to delete items in index '{_index}'. Reason: {response.ServerError?.Error?.Reason}");
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while deleting items in index '{_index}': {ex.Message}");
				return false;
			}
		}

		public async Task<List<ElasticCategoryCountDTO>> GetElasticProductBrandCount(string category)
		{
			{
				try
				{

                    List<ElasticCategoryCountDTO> elasticCategoryCountDTO = new List<ElasticCategoryCountDTO>();

					var searchResponse = _client.Search<ElasticProductDTO>(s => s
						.Size(0)
						.Aggregations(a => a
							.Filter("brands", f => f
								.Filter(q => q
									.Term(t => t
										.Field("category")
										.Value(category)
									)
								)
								.Aggregations(aa => aa
									.Terms("byBrand", t => t
										.Field("brand")
										.Size(20)
									)
								)
							)
						)
					);

					if (searchResponse.IsValid)
					{
						var brandsAgg = searchResponse.Aggregations.Filter("brands");
						var byBrandAgg = brandsAgg.Terms("byBrand");
						foreach (var brandBucket in byBrandAgg.Buckets)
						{
                            await Console.Out.WriteLineAsync(brandBucket.Key);
                            elasticCategoryCountDTO.Add(new ElasticCategoryCountDTO
							{
								name = brandBucket.Key,
								value = (long)brandBucket.DocCount
							});
						}

						return elasticCategoryCountDTO;
					}
					else
					{
						throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetElasticProductCategoriesCount", "No Product"));
					}

				}
				catch (Exception ex)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetElasticProductCategoriesCount", ex.Message));
				}
			}
		}

		public async Task<List<ElasticCategoryCountDTO>> GetElasticProductCategoriesCount()
		{
			try
			{
				List<ElasticCategoryCountDTO> elasticCategoryCountDTO = new List<ElasticCategoryCountDTO>();

				var searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
					.Size(0)
					.Aggregations(a => a
						.Terms("categories", t => t
							.Field(f => f.category)
							.Size(20))
						)
					);

				if (searchResponse.IsValid)
				{
					var categories = searchResponse.Aggregations.Terms("categories").Buckets;

					foreach (var category in categories)
					{
						elasticCategoryCountDTO.Add(
								new ElasticCategoryCountDTO
								{
									name = category.Key,
									value = (long)category.DocCount
								});
					}

					return elasticCategoryCountDTO;
				}
				else
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetElasticProductCategoriesCount", "No Product"));
				}

			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetElasticProductCategoriesCount", ex.Message));
			}

		}

		public async Task<ElasticSearchModelDTO> GetElasticProductModelCount(string category, string model)
		{
			try
			{
				ElasticSearchModelDTO elasticSearchModelDTOs = new ElasticSearchModelDTO();
				elasticSearchModelDTOs.data = new List<string>();
				elasticSearchModelDTOs.value = new List<long>();

				var searchResponse = _client.Search<ElasticProductDTO>(s => s
					.Size(0)
					.Aggregations(a => a
						.Filter("brands", f => f
							.Filter(a => a
								.Bool(b => b
									.Must(
										m => m.Term(t => t.Field("category").Value(category)),
										m => m.Term(t => t.Field("brand").Value(model))
									)
								)
							)
								.Aggregations(aa => aa
									.Terms("byModel", t => t
										.Field("model")
										.Size(20)
									)
								)
							
						)
					)
				);

				if (searchResponse.IsValid)
				{
					var brandsAgg = searchResponse.Aggregations.Filter("brands");
					var byModelAgg = brandsAgg.Terms("byModel");

					foreach (var bucket in byModelAgg.Buckets)
					{
						elasticSearchModelDTOs.data.Add(bucket.Key);
						elasticSearchModelDTOs.value.Add((long)bucket.DocCount);
					}

					return elasticSearchModelDTOs;
				}
				else
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetElasticProductCategoriesCount", "No Product"));
				}

			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetElasticProductCategoriesCount", ex.Message));
			}
		}

		public async Task<List<ElasticProductDTO>> GetLowPriceElasticProduct(ProductSearchTermDTO searchTermDTO)
		{

			// Func<Descriptor<ElasticProductDTO>>
			// Used to describe in a fluent and type-safe manner, with document type of ElasticProductDTO
			var filterClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var shouldClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var mustClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var mustClauses2 = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();

			Func<SortDescriptor<ElasticProductDTO>, SortDescriptor<ElasticProductDTO>> sortClauses = null;

			// 1. must Title
			if (!string.IsNullOrEmpty(searchTermDTO.description))
			{
				mustClauses.Add(must => must.Match(m => m.Field(f => f.title).Query(searchTermDTO.description)));
				mustClauses2.Add(must => must.Match(m => m.Field(f => f.description).Query(searchTermDTO.description)));
			}

			// 2. must Category
			if (!string.IsNullOrEmpty(searchTermDTO.category))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.category).Value(searchTermDTO.category)));
			}

			// 3. must Brand
			if (!string.IsNullOrEmpty(searchTermDTO.brand))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.brand).Value(searchTermDTO.brand)));
			}

			// 4. must Model
			if (!string.IsNullOrEmpty(searchTermDTO.model))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.model).Value(searchTermDTO.model)));
			}

			// 5. should Description
			if (!string.IsNullOrEmpty(searchTermDTO.description))
			{
				shouldClauses.Add(should => should.Match(m => m.Field(f => f.description).Query(searchTermDTO.description)));
			}

			// 6. Price range
			if (searchTermDTO.highest_price.HasValue)
			{
				shouldClauses.Add(should => should.Range(r => r.Field(f => f.price).LessThanOrEquals((double?)searchTermDTO.highest_price)));
			}
			if (searchTermDTO.lowest_price.HasValue)
			{
				shouldClauses.Add(should => should.Range(r => r.Field(f => f.price).LessThanOrEquals((double?)searchTermDTO.lowest_price)));
			}

			// 7. must Country
			if (!string.IsNullOrEmpty(searchTermDTO.country))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.country).Value(searchTermDTO.country)));
			}

			// 8. must State
			if (!string.IsNullOrEmpty(searchTermDTO.state))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.state).Value(searchTermDTO.state)));
			}

			// 9. must Condition
			if (!string.IsNullOrEmpty(searchTermDTO.condition))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.condition).Value(searchTermDTO.condition)));
			}

			// 10. should Spider
			if (searchTermDTO.spider != null)
			{
				shouldClauses.Add(should => should.Terms(t => t.Field(f => f.spider).Terms(searchTermDTO.spider)));
			}

			// 11. Sort
			if (!string.IsNullOrEmpty(searchTermDTO.sort))
			{
				if (searchTermDTO.sort == "priceasc")
					sortClauses = ss => ss.Ascending(p => p.price);

				if (searchTermDTO.sort == "pricedesc")
					sortClauses = ss => ss.Descending(p => p.price);
			}

			try
			{
				ISearchResponse<ElasticProductDTO> searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
				.Index("product")
				.Query(q => q
				.Bool(b => b
					.Should(shouldClauses)
					.Filter(filterClauses)
					.Must(mustClauses)
				)
				)
				.From(0)
				.Size(5)
				.Sort(sortClauses)
				);


				List<ElasticProductDTO> productList = new List<ElasticProductDTO>();

				foreach (var hit in searchResponse.Hits)
				{
					productList.Add(hit.Source);
				}

				return productList;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "SearchProduct", ex.Message));
			}

		}

		public async Task<List<ElasticProductDTO>> GetLowPriceProductForPriceHistory(ProductSearchTermDTO productSearchTermDTO)
		{
			// Func<Descriptor<ElasticProductDTO>>
			// Used to describe in a fluent and type-safe manner, with document type of ElasticProductDTO
			var filterClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var mustClause = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();

			Func<SortDescriptor<ElasticProductDTO>, SortDescriptor<ElasticProductDTO>> sortClauses = null;

			List<ElasticProductDTO> listOfProduct = new List<ElasticProductDTO>();

			// 2. must Category
			if (!string.IsNullOrEmpty(productSearchTermDTO.category))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.category).Value(productSearchTermDTO.category)));
			}

			// 3. must Brand
			if (!string.IsNullOrEmpty(productSearchTermDTO.brand))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.brand).Value(productSearchTermDTO.brand)));
			}

			// 4. must Model
			if (!string.IsNullOrEmpty(productSearchTermDTO.model))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.model).Value(productSearchTermDTO.model)));
			}

			// 11. sort Price
			sortClauses = ss => ss.Ascending(p => p.price_CNY);

			if (_spiders != null)
			{
				try
				{

					foreach (string spider in _spiders)
					{

						await Console.Out.WriteLineAsync(spider);
						mustClause.Add(must => must.Match(t => t.Field(f => f.spider).Query(spider)));

						try
						{

							ISearchResponse<ElasticProductDTO> searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
								.Index("product")
								.Query(q => q
								.Bool(b => b
									.Filter(filterClauses)
									.Must(mustClause)
								)
								)
								.Size(1)
								.Sort(sortClauses));

							// Check if the search was successful and if there are any results
							if (searchResponse.IsValid && searchResponse.Hits.Count > 0)
							{
								await Console.Out.WriteLineAsync(searchResponse.Hits.First().Source.spider);
								listOfProduct.Add(searchResponse.Hits.First().Source);
							}
							else
							{
								// Handle the case where no products are found
								Console.WriteLine($"No products found for spider {spider}.");
							}

						}
						catch (Exception ex)
						{
							throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "GetLowPriceProductForPriceHistory", ex.Message));
						}


						mustClause = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
					}

					return listOfProduct;
				}
				catch
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetLowPriceProductForPriceHistory", "spider"));
				}
			}
			throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetLowPriceProductForPriceHistory", "no spider"));
		}

		// TODO 增强这边的算法，如何去比价（根据商品的品质）
		public async Task<List<ElasticProductDTO>> GetLowPriceProductForUserSubscribe(UserSubscription userSubscription)
		{

			// Func<Descriptor<ElasticProductDTO>>
			// Used to describe in a fluent and type-safe manner, with document type of ElasticProductDTO
			var filterClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();

			int shouldCount = 0;

			var shouldClausesCondition = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var shouldClausesDescription = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var shouldClausesSpider = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();

			var mustClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();

			Func<SortDescriptor<ElasticProductDTO>, SortDescriptor<ElasticProductDTO>> sortClauses = null;

			// 1. must include user description
			if (!string.IsNullOrEmpty(userSubscription.UserSubscriptionProductDescription))
			{
				shouldCount += 1;
				// Title have user description
				shouldClausesDescription.Add(should => should.Match(m => m.Field(f => f.title).Query(userSubscription.UserSubscriptionProductDescription)));

				// Description have user description
				shouldClausesDescription.Add(should => should.Match(m => m.Field(f => f.description).Query(userSubscription.UserSubscriptionProductDescription)));
			}

			// 2. must Category
			if (!string.IsNullOrEmpty(userSubscription.UserSubscriptionProductCategory))
			{
				mustClauses.Add(must => must.Term(term => term.Field(f => f.category).Value(userSubscription.UserSubscriptionProductCategory)));
			}

			// 3. must Brand
			if (!string.IsNullOrEmpty(userSubscription.UserSubscriptionProductBrand))
			{
				mustClauses.Add(must => must.Term(term => term.Field(f => f.brand).Value(userSubscription.UserSubscriptionProductBrand)));
			}

			// 4. must Model
			if (!string.IsNullOrEmpty(userSubscription.UserSubscriptionProductModel))
			{
				mustClauses.Add(must => must.Term(term => term.Field(f => f.model).Value(userSubscription.UserSubscriptionProductModel)));
			}

			// 5. Price range
			if (userSubscription.UserSubscriptionProductHighestPrice.HasValue)
			{
				mustClauses.Add(must => must.Range(r => r.Field(f => f.price_CNY).LessThanOrEquals((double?)userSubscription.UserSubscriptionProductHighestPrice)));
			}
			//if (userSubscription.UserSubscriptionProductLowestPrice.HasValue)
			//{
			//	mustClauses.Add(must => must.Range(r => r.Field(f => f.price).GreaterThanOrEquals((double?)userSubscription.UserSubscriptionProductLowestPrice)));
			//}

			// 7. must Country
			if (!string.IsNullOrEmpty(userSubscription.UserSubscriptionProductCountry))
			{
				mustClauses.Add(must => must.Term(term => term.Field(f => f.country).Value(userSubscription.UserSubscriptionProductCountry)));
			}

			// 8. must State
			if (!string.IsNullOrEmpty(userSubscription.UserSubscriptionProductState))
			{
				mustClauses.Add(must => must.Term(term => term.Field(f => f.state).Value(userSubscription.UserSubscriptionProductState)));
			}

			// 9. should Condition
			if (!string.IsNullOrEmpty(userSubscription.UserSubscriptionProductCondition))
			{
				shouldCount += 1;

				// used 
				switch (userSubscription.UserSubscriptionProductCondition)
				{
					case "used":
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("used")));
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("mint")));
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("new")));
						break;
					case "mint":
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("mint")));
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("new")));
						break;
					case "new":
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("new")));
						break; 
					default:
						throw new Exception("No responding condition " + userSubscription.UserSubscriptionProductCondition);
				}

			}

			// 10. should Spider
			if (userSubscription.UserSubscriptionSpiders != null)
			{
				try
				{
					shouldCount += 1;

					string[] spiders = userSubscription.UserSubscriptionSpiders.Split(",");

					foreach (string spider in spiders)
					{
						shouldClausesSpider.Add(should => should.Match(t => t.Field(f => f.spider).Query(userSubscription.UserSubscriptionSpiders)));
					}
				}
				catch
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetLowPriceProductForUserSubscribe", "Error splitting spiders"));
				}

			}

			// 11. sort Price
			sortClauses = ss => ss.Ascending(p => p.price_CNY);

			try
			{
				List<ElasticProductDTO> productList = new List<ElasticProductDTO>();


					ISearchResponse<ElasticProductDTO> searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
					.Index("product")
					.Query(q => q
					.Bool(b => b
						.Must(mustClauses)
						.Should(sh => sh
							.Bool(b1 => b1
								.Should(shouldClausesDescription).MinimumShouldMatch(shouldClausesDescription.Count > 0 ? 1 : 0)
								), sh2 => sh2
							.Bool(b2 => b2
								.Should(shouldClausesCondition).MinimumShouldMatch(shouldClausesDescription.Count > 0 ? 1 : 0)
								), sh3 => sh3
							.Bool(b3 => b3
								.Should(shouldClausesSpider).MinimumShouldMatch(shouldClausesDescription.Count > 0 ? 1 : 0)
								)
							).MinimumShouldMatch(shouldCount)
						)
					)
					.From(0)
					.Size(5)
					.Sort(sortClauses)
					);

					await Console.Out.WriteLineAsync(searchResponse.Hits.Count.ToString());

					foreach (var hit in searchResponse.Hits)
					{
						productList.Add(hit.Source);
					}

					await Console.Out.WriteLineAsync(productList.Count.ToString());

				

				return productList;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "SearchProduct", ex.Message));
			}
		}

		public async Task<ElasticProductDTO> GetOneLowestPriceProduct(ProductSearchTermDTO productSearchTermDTO)
		{
			// Func<Descriptor<ElasticProductDTO>>
			// Used to describe in a fluent and type-safe manner, with document type of ElasticProductDTO
			var filterClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var shouldClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();

			Func<SortDescriptor<ElasticProductDTO>, SortDescriptor<ElasticProductDTO>> sortClauses = null;


			// 2. must Category
			if (!string.IsNullOrEmpty(productSearchTermDTO.category))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.category).Value(productSearchTermDTO.category)));
			}

			// 3. must Brand
			if (!string.IsNullOrEmpty(productSearchTermDTO.brand))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.brand).Value(productSearchTermDTO.brand)));
			}

			// 4. must Model
			if (!string.IsNullOrEmpty(productSearchTermDTO.model))
			{
				filterClauses.Add(must => must.Term(term => term.Field(f => f.model).Value(productSearchTermDTO.model)));
			}

			// 11. sort Price
			sortClauses = ss => ss.Ascending(p => p.price_CNY);

			if (productSearchTermDTO.spider != null)
			{
				try
				{

					foreach (string spider in productSearchTermDTO.spider)
					{
						shouldClauses.Add(should => should.Match(t => t.Field(f => f.spider).Query(spider)));
					}
				}
				catch
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetOneLowestPriceProduct", "spider"));
				}
			}

			try
			{

				ISearchResponse<ElasticProductDTO> searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
					.Index("product")
					.Query(q => q
					.Bool(b => b
						.Filter(filterClauses)
						.Should(shouldClauses)
					)
					)
					.Size(1)
					.Sort(sortClauses));

				// Check if the search was successful and if there are any results
				if (searchResponse.IsValid && searchResponse.Hits.Count > 0)
				{
					// Return the product with the lowest price
					return searchResponse.Hits.First().Source;
				}
				else
				{
					// Handle the case where no products are found
					Console.WriteLine("No products found.");
					return null;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "GetOneLowestPriceProduct", ex.Message));
			}
		}

		public async Task<List<ElasticProductDTO>> GetPaginationElasticProduct(ProductSearchTermDTO searchTermDTO, PageDTO pageDTO)
		{
			int shouldMatch = 0;

			await Console.Out.WriteLineAsync(searchTermDTO.ToString());

			// Func<Descriptor<ElasticProductDTO>>
			// Used to describe in a fluent and type-safe manner, with document type of ElasticProductDTO
			var mustClauses = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var shouldClausesDescription = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var shouldClausesCondition = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			var shouldClausesSpiders = new List<Func<QueryContainerDescriptor<ElasticProductDTO>, QueryContainer>>();
			Func<SortDescriptor<ElasticProductDTO>, SortDescriptor<ElasticProductDTO>> sortClauses = null;

			// 1. should one description
			if (!string.IsNullOrEmpty(searchTermDTO.description))
			{
				shouldMatch += 1;
				// Title have user description
				shouldClausesDescription.Add(should => should.Match(m => m.Field(f => f.title).Query(searchTermDTO.description)));

				// Description have user description
				shouldClausesDescription.Add(should => should.Match(m => m.Field(f => f.description).Query(searchTermDTO.description)));
			}

			// 2. must Category
			if (!string.IsNullOrEmpty(searchTermDTO.category))
			{
				await Console.Out.WriteLineAsync(searchTermDTO.category);
				mustClauses.Add(must => must.Match(m => m.Field(f => f.category).Query(searchTermDTO.category)));
			}

			// 3. must Brand
			if (!string.IsNullOrEmpty(searchTermDTO.brand))
			{
				await Console.Out.WriteLineAsync(searchTermDTO.brand);
				mustClauses.Add(must => must.Match(m => m.Field(f => f.brand).Query(searchTermDTO.brand)));
			}

			// 4. must Model
			if (!string.IsNullOrEmpty(searchTermDTO.model))
			{
				await Console.Out.WriteLineAsync(searchTermDTO.model);
				mustClauses.Add(must => must.Match(m => m.Field(f => f.model).Query(searchTermDTO.model)));
			}


			// 5. Price range
			//if (searchTermDTO.highest_price.HasValue)
			//{
			//	mustClauses.Add(must => must.Range(r => r.Field(f => f.price_CNY).LessThanOrEquals((double?)searchTermDTO.highest_price)));
			//}


			// 7. must Country
			//if (!string.IsNullOrEmpty(searchTermDTO.country))
			//{
			//	filterClauses.Add(must => must.Term(term => term.Field(f => f.country).Value(searchTermDTO.country)));
			//}

			// 8. must State
			//if (!string.IsNullOrEmpty(searchTermDTO.state))
			//{
			//	filterClauses.Add(must => must.Term(term => term.Field(f => f.state).Value(searchTermDTO.state)));
			//}

			// 9. must Condition
			if (!string.IsNullOrEmpty(searchTermDTO.condition))
			{
				shouldMatch += 1;
				// used 
				switch (searchTermDTO.condition)
				{
					case "used":
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("used")));
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("mint")));
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("new")));
						break;
					case "mint":
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("mint")));
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("new")));
						break;
					case "new":
						shouldClausesCondition.Add(should => should.Match(t => t.Field(f => f.condition).Query("new")));
						break;
					default:
						throw new Exception("No responding condition " + searchTermDTO.condition);
				}

			}

			// 10. should Spider
			if (searchTermDTO.spider != null)
			{
				shouldMatch += 1;
				try
				{
					foreach (string spider in searchTermDTO.spider)
					{
						await Console.Out.WriteLineAsync(spider);
						shouldClausesSpiders.Add(should => should.Match(t => t.Field(f => f.spider).Query(spider)));
					}

				}
				catch
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetPaginationElasticProduct", "spider"));
				}
			}

			// 11. Sort
			if (!string.IsNullOrEmpty(searchTermDTO.sort))
			{
				if (searchTermDTO.sort == "priceasc")
					sortClauses = ss => ss.Ascending(p => p.price_CNY);

				if (searchTermDTO.sort == "pricedesc")
					sortClauses = ss => ss.Descending(p => p.price_CNY);
			}

			try
			{
				await Console.Out.WriteLineAsync(pageDTO.CurrentPage.ToString());
				await Console.Out.WriteLineAsync(pageDTO.PageSize.ToString());
                await Console.Out.WriteLineAsync(shouldMatch.ToString());

                ISearchResponse<ElasticProductDTO> searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
				.Index("product")
				.Query(q => q
				.Bool(b => b
						.Must(mustClauses)
						.Should(sh => sh
							.Bool(b1 => b1
								.Should(shouldClausesDescription).MinimumShouldMatch(shouldClausesDescription.Count > 0 ? 1: 0)
								), sh2 => sh2
							.Bool(b2 => b2
								.Should(shouldClausesCondition).MinimumShouldMatch(shouldClausesCondition.Count > 0 ? 1 : 0)
								), sh3 => sh3
							.Bool(b3 => b3
								.Should(shouldClausesSpiders).MinimumShouldMatch(shouldClausesSpiders.Count > 0 ? 1 : 0)
								)
							).MinimumShouldMatch(shouldMatch)
						)
				)
				.From(((pageDTO.CurrentPage - 1) * pageDTO.PageSize))
				.Size(pageDTO.PageSize)
				.Sort(sortClauses)
				);


				List<ElasticProductDTO> productList = new List<ElasticProductDTO>();
                await Console.Out.WriteLineAsync(productList.Count().ToString());

				foreach (var hit in searchResponse.Hits)
				{
                    productList.Add(hit.Source);
				}

				return productList;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "SearchProduct", ex.Message));
			}

		}

		public async Task<List<ElasticProductDTO>> GetProductOver7Days()
		{
			try
			{
				DateTime sevenDaysAgo = DateTime.Now.AddDays(-7);


				ISearchResponse<ElasticProductDTO> searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
					.Index(_index)
					.Query(q => q
						.Bool(b => b
							.Filter(f => f
								.DateRange(r => r
									.Field(f => f.scraped_date)
									.LessThanOrEquals(sevenDaysAgo)
									)
								)

							)
						)
					.Size(300)
					);

				await Console.Out.WriteLineAsync(searchResponse.Hits.Count.ToString());

				List<ElasticProductDTO> productsOver7Days = new List<ElasticProductDTO>();

				foreach (var hit in searchResponse.Hits)
				{
					productsOver7Days.Add(hit.Source);
				}

				return productsOver7Days;

			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ElasticSearchDAO", "GetProductOver7Days", ex.Message));
			}
		}

		public async Task<PageEntity<ElasticProductDTO>> GetRandom10Product(PageDTO pageDTO, string category)
		{
			try
			{
				var searchResponse = _client.Search<ElasticProductDTO>(s =>
				s.MatchAll()
				.Size(pageDTO.PageSize)
				.From(pageDTO.CurrentPage * pageDTO.PageSize)
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(query => query
							.Bool(b => b
								.Must(f => f
									.Term(t => t.category, category))))
						.Functions(fsf => fsf
							.RandomScore()
							)
						.BoostMode(FunctionBoostMode.Replace)
						)
					)
				);

				Console.WriteLine(searchResponse.Hits.Count);

				List<ElasticProductDTO> productList = new List<ElasticProductDTO>();

				foreach (var product in searchResponse.Hits)
				{
					productList.Add(product.Source);
				}

				return new PageEntity<ElasticProductDTO>
				{
					Count = productList.Count,
					rows = productList
				};
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "GetRandom10Product", ex.Message));
			}
		}

		public async Task<List<ElasticProductDTO>> GetUserFavourite(List<string> productUniqueIds)
		{
			List<ElasticProductDTO> favouriteProducts = new List<ElasticProductDTO>();

			await Console.Out.WriteLineAsync("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

			try
			{
				foreach (var productId in productUniqueIds)
				{
					// Search for the product with the given productId
					var searchResponse = await _client.SearchAsync<ElasticProductDTO>(s => s
						.Index("product")
						.Query(q => q
							.Term(t => t
								.Field(f => f.unique_id)
								.Value(productId)
							)
						)
						.Size(1) // Assuming each productId is unique
					);

					// Check if the product exists in Elasticsearch
					if (searchResponse.IsValid && searchResponse.Hits.Count > 0)
					{
						// Add the product to the list of favourite products
						favouriteProducts.Add(searchResponse.Hits.First().Source);
					}
					else
					{
						// Handle the case where the product is not found
						Console.WriteLine($"Product with ID {productId} not found.");
					}
				}

				return favouriteProducts;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "GetUserFavourite", ex.Message));
			}
		}

		public async Task<bool> RemoveOneProduct(string unique_id)
		{
			try
			{
				var response = await _client.DeleteAsync<ElasticProductDTO>(unique_id, d => d
					.Index(_index) // Specify the index from which you want to delete the product
					);

				if (response.IsValid)
				{
					Console.WriteLine($"Product with unique ID '{_index}' deleted successfully from index '{_index}'.");
					return true;
				}
				else
				{
					Console.WriteLine($"Failed to delete product with unique ID '{_index}' from index '{_index}'. Reason: {response.ServerError?.Error?.Reason}");
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while deleting product with unique ID '{_index}' from index '{_index}': {ex.Message}");
				return false;
			}
		}

		public async Task<bool> UpdateProduct(ElasticProductDTO updatedProduct)
		{
			try
			{
				var response = await _client.UpdateAsync<ElasticProductDTO, ElasticProductDTO>(updatedProduct.unique_id, u => u
					.Index(_index)
					.Doc(updatedProduct)
					.RetryOnConflict(3));

				if (response.IsValid)
				{
					Console.WriteLine($"Product with ID {updatedProduct.unique_id} updated successfully.");
					return true;
				}
				else
				{
					Console.WriteLine($"Failed to update product with ID {updatedProduct.unique_id}. Reason: {response.ServerError?.Error?.Reason}");
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while updating the product: {ex.Message}");
				return false;
			}
		}
	}
}


