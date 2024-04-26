using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Controllers;
using webapi.Models;
using webapi.Services.EmailService;
using webapi.Services.JwtService;
using webapi.Services.ProductService;
using webapi.Services.SubscribedItemService;
using webapi.Services.UserService;
using webapi.Services.UserSubscriptionService;
using webapi.Services.UtilitiesService;

namespace webapi.Tests.Controller
{
	public class UserControllerTests
	{
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;
		private readonly IEmailService _emailService;
		private readonly IJwtService _jwtService;
		private readonly IUtilitiesService _utilitiesService;
		private readonly IProductService _productService;
		private readonly IUserSubscriptionService _userSubscriptionService;
		private readonly ISubscribedItemService _subscribedItemService;

		public UserControllerTests()
		{
			_configuration = A.Fake<IConfiguration>();
			_userService = A.Fake<IUserService>();
			_emailService = A.Fake<IEmailService>();
			_jwtService = A.Fake<IJwtService>();
			_utilitiesService = A.Fake<IUtilitiesService>();
			_productService = A.Fake<IProductService>();
			_userSubscriptionService = A.Fake<IUserSubscriptionService>();
			_subscribedItemService = A.Fake<ISubscribedItemService>();
		}

		[Theory]
		[InlineData("CorrectEmail", "ValidVerifCode", typeof(OkObjectResult))]
		[InlineData("InvalidEmail", "", typeof(BadRequestObjectResult))]
		public async void UserController_GetCode_ReturnOk(string email, string verificationCode, Type expectedType)
		{
			//Arrange
			var userVerificationCode = A.Fake<UserVerificationCode>(x => x.WithArgumentsForConstructor(() => new UserVerificationCode(email, verificationCode, DateTime.Now)));

			A.CallTo(() => _emailService.SendVerificationCode(email)).Returns(Task.FromResult(verificationCode));
			A.CallTo(() => _userService.SaveVerificationCode(email, verificationCode)).Returns(Task.FromResult(userVerificationCode));

			var controller = new UserController(_configuration, _userService, _emailService, _jwtService, _utilitiesService,
				_productService, _userSubscriptionService, _subscribedItemService);

			//Act
			var result = await controller.GetCode(email);

			//Assert
			result.Should().BeOfType(expectedType);

		}
    }
}
