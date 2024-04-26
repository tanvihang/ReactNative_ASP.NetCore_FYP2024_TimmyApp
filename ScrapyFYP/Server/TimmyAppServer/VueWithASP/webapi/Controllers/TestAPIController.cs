using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services.UserService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestAPIController : ControllerBase
	{
        TimmyDbContext _context;
        IUserService _userService;

        public TestAPIController(TimmyDbContext timmyDbContext, IUserService userService)
        {
            _context = timmyDbContext;
            _userService = userService;
        }


        
    }
}
