using System.Text.RegularExpressions;

namespace webapi.Utilities
{
	public static class StaticGenerator
	{
		public static string GenerateId(string prefix)
		{
			string generatedId = prefix + Guid.NewGuid().ToString();

			return generatedId;
		}

		public static string GenerateDTOErrorMessage(string className, string methodName, string message)
		{
			string msg = $"Error occured in {className} : {methodName} - {message}";
			return msg;
		}

		public static string GenerateServiceErrorMessage(string className, string methodName, string message)
		{
			string msg = $"Error occured in {className} : {methodName} - {message}";
			return msg;
		}

		public static string GenerateProductFullName(string category, string brand, string model)
		{
			string fullname = category + " " + brand + " " + model;
			return fullname;
		}

		public static bool IsValidEmail(string email)
		{
			// 使用正则表达式检查电子邮件地址的格式
			string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
			return Regex.IsMatch(email, pattern);
		}
	}
}
