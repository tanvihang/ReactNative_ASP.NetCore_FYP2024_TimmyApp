namespace webapi.Models.HangFireResponse
{
	public class HangFireExecuteUpdateElasticProductDTO
	{
		public int? updatedElasticProductPrice { get; set; } = 0;
		public int? failedRequest { get; set; } = 0;
		public int? removedProduct {  get; set; } = 0;
	}
}
