using System.Diagnostics.CodeAnalysis;
using webapi.Models.DTO;

namespace webapi.Utilities.Linq
{
	public class ElasticProductDTOUniqueIdComparer : IEqualityComparer<ElasticProductDTO>
	{
		public bool Equals(ElasticProductDTO? x, ElasticProductDTO? y)
		{
			return x.unique_id == y.unique_id;
		}

		public int GetHashCode([DisallowNull] ElasticProductDTO obj)
		{
			return obj.unique_id.GetHashCode();
		}
	}
}
