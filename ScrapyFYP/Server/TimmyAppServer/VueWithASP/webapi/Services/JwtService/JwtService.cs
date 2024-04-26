using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using webapi.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace webapi.Services.JwtService
{
	public class JwtService : IJwtService
	{
		private readonly string secretKey;

		private readonly IConfiguration _configuration;
		private readonly TimmyDbContext _context;

		public JwtService(IConfiguration configuration, TimmyDbContext timmyDbContext)
		{
			_configuration = configuration;
			_context = timmyDbContext;

			secretKey = _configuration["JWT:secretkey"]!;
		}

		public string GenerateJwtToken(string userId, string username, int expirationDay)
		{

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);


			// State the Payload, Header of the token
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, userId),
					new Claim(ClaimTypes.Name, username)
				}),
				Expires = DateTime.UtcNow.AddDays(expirationDay),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			};

			// Generate token based on the tokenDescriptor
			var token = tokenHandler.CreateToken(tokenDescriptor);
			
			return tokenHandler.WriteToken(token);
		}

		public Boolean ValidateJwtToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			string secretKey = _configuration["JWT:secretkey"];
			var key = Encoding.ASCII.GetBytes(secretKey);

			//生成验证参数
			var validationParameter = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero,
			};

			//进行验证
			try
			{
				SecurityToken validatedToken;
				tokenHandler.ValidateToken(token, validationParameter, out validatedToken);
				return true;
			}
			catch(Exception ex)
			{
                return false;
			}
		}

		public string ParseJwtToUserId(string jwtToken)
		{
			var handler = new JwtSecurityTokenHandler();

			var tokenDeserialized = handler.ReadJwtToken(jwtToken);

			try
			{
				var claim = tokenDeserialized.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

				return claim;

			}
			catch(Exception ex)
			{
                return null;
			}
		}
	}
}
