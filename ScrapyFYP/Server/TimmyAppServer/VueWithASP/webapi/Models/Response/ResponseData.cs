using System.Text.Json.Serialization;

namespace webapi.Models.Response
{
	public class ResponseData<T>
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }


		public T? Data { get; set; }

		public static ResponseData<T> Success(T data)
		{
			return new ResponseData<T>
			{
				StatusCode = 200,
				Message = "Success",
				Data = data
			};
		}

		public static ResponseData<T> Success(string? message = "Success")
		{
			return new ResponseData<T>
			{
				StatusCode = 200,
				Message = message
			};
		}

		public static ResponseData<T> Success(T data, string? message = "Success")
		{
			return new ResponseData<T>
			{
				StatusCode = 200,
				Message = message,
				Data = data
			};
		}

		public static ResponseData<T> Failure(string? message = "Failure")
		{
			return new ResponseData<T>
			{
				StatusCode = 400,
				Message = message
			};
		}
	}
}
