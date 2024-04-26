using webapi.Models.DTO;

namespace webapi.Services.PlatformService
{
	public interface IPlatformService
	{
		Task<RequestResponseDTO> SendRequest(string url);
		decimal RetrievePrice(string content, string platform);

	}
}
