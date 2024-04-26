using webapi.Models;

namespace webapi.Services.JwtService
{
	public interface IJwtService
	{
		//Jwt token 
		//Header: {
		//  "alg": "HS256",
		//  "typ": "JWT"
		//}
		//Payload: {
		//  "sub": "1234567890",
		//  "name": "John Doe",
		//  "admin": true,
		//  "exp": 1613003200 // 过期时间
		//}
		//Signature: [Signature]

		public string GenerateJwtToken(string userId, string username, int expirationDay);
		public Boolean ValidateJwtToken(string token);
		public string ParseJwtToUserId(string token);

	}
}
