using Hangfire;
using Microsoft.EntityFrameworkCore;
using Nest;
using Python.Runtime;
using webapi.DAO.BloomFilterDAO;
using webapi.DAO.ElasticSearchDAO;
using webapi.DAO.NotificationDAO;
using webapi.DAO.PriceHistoryDAO;
using webapi.DAO.ScraperDAO;
using webapi.DAO.SubscribedProductDAO;
using webapi.DAO.TimmyProductDAO;
using webapi.DAO.UserFavouriteDAO;
using webapi.DAO.UserSearchHistoryDAO;
using webapi.DAO.UserSubscriptionDAO;
using webapi.DAO.UserSubscriptionProductDAO;
using webapi.DAO.UserTDAO;
using webapi.DAO.UserVerificationCodeDAO;
using webapi.Models;
using webapi.Services.BloomFilterService;
using webapi.Services.DailyService;
using webapi.Services.ElasticSearchService;
using webapi.Services.JwtService;
using webapi.Services.NotificationService;
using webapi.Services.PlatformService;
using webapi.Services.PriceHistoryService;
using webapi.Services.ScraperService;
using webapi.Services.SignalService;
using webapi.Services.SubscribedProductService;
using webapi.Services.TimmyProductService;
using webapi.Services.UserFavouriteService;
using webapi.Services.UserSearchHistory;
using webapi.Services.UserSearchHistoryService;
using webapi.Services.UserService;
using webapi.Services.UserSubscriptionProductService;
using webapi.Services.UserSubscriptionService;
using webapi.Services.UserVerificationCodeService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigin",
		builder => builder.AllowAnyOrigin() // 指定你的前端应用的URL
						  .AllowAnyMethod()
						  .AllowAnyHeader());
});

// Search for controller that have attributes [ApiController]
// 添加控制器
builder.Services.AddControllers();

// 添加数据库
builder.Services.AddDbContext<TimmyDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection2")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加Hangfire服务
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();


//添加数据持久层
builder.Services.AddScoped<IUserTDAO, UserTDAO>();
builder.Services.AddScoped<IUserVerificationCodeDAO, UserVerificationCodeDAO>();
builder.Services.AddScoped<INotificationDAO,  NotificationDAO>();
builder.Services.AddScoped<ITimmyProductDAO, TimmyProductDAO>();
builder.Services.AddScoped<IElasticSearchDAO, ElasticSearchDAO>();
builder.Services.AddScoped<ISubscribedProductDAO, SubscribedProductDAO>();
builder.Services.AddScoped<IUserSubscriptionDAO, UserSubscriptionDAO>();
builder.Services.AddScoped<IScraperDAO, ScraperDAO>();
builder.Services.AddScoped<IUserSearchHistoryDAO, UserSearchHistoryDAO>();
builder.Services.AddScoped<IUserSubscriptionProductDAO, UserSubscriptionProductDAO>();
builder.Services.AddScoped<IUserFavouriteDAO, UserFavouriteDAO>();
builder.Services.AddScoped<IPriceHistoryDAO, PriceHistoryDAO>();
builder.Services.AddScoped<IBloomFilterDAO, BloomFilterDAO>();

// 添加业务层
// AddScoped(存在一个Service而已,每一次请求都是独立的，无状态，除非你定义的是static) / AddTransient（可以有多个Service Instance）
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserVerificationCodeService, UserVerificationCodeService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITimmyProductService, TimmyProductService>();
builder.Services.AddScoped<IElasticSearchService, ElasticSearchService>();
builder.Services.AddScoped<ISubscribedProductService, SubscribedProductService>();
builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
builder.Services.AddScoped<IScraperService, ScraperService>();
builder.Services.AddScoped<IUserSearchHistoryService, UserSearchHistoryService>();
builder.Services.AddScoped<IUserSubscriptionProductService, UserSubscriptionProductService>();
builder.Services.AddScoped<ISignalService, SignalService>();
builder.Services.AddScoped<IUserFavouriteService, UserFavouriteService>();
builder.Services.AddScoped<IPriceHistoryService,  PriceHistoryService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IBloomFilterService, BloomFilterService>();

// 添加其他业务
builder.Services.AddScoped<IJwtService, JwtService>();

//将log转换成JSON形式
builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new()
    {
        Indented = true,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigin");
app.MapControllers();

app.UseHangfireDashboard();
app.MapHangfireDashboard("/hangfire");

using(var serviceScope = app.Services.CreateScope())
{
	var services = serviceScope.ServiceProvider;

    ISignalService signalService = services.GetRequiredService<ISignalService>();
	Console.WriteLine("Adding recurring job");

	// Auto update
	RecurringJob.AddOrUpdate("ExecuteUpdateElasticProduct", () => signalService.ExecuteUpdateElasticProduct(), Cron.HourInterval(1));

	// Create hangfire for these jobs
	RecurringJob.AddOrUpdate("ExecuteScrapeAndGetLowestPrice", () => signalService.ExecuteScrapeAndGetLowestPrice(), Cron.Weekly);

	for (int i = 0; i < 24; i++)
	{
		RecurringJob.AddOrUpdate($"ExecuteSearchBestUserSubscribedProduct_{i}", () => signalService.ExecuteSearchBestUserSubscribedProduct(i), Cron.Daily(i));
	}

	// Subscribed product based on level
	RecurringJob.AddOrUpdate("ExecuteScrapeSubscribedProductLevel3", () => signalService.ExecuteScrapeSubscribedProduct(3), Cron.Daily);
	RecurringJob.AddOrUpdate("ExecuteScrapeSubscribedProductLevel2", () => signalService.ExecuteScrapeSubscribedProduct(2), Cron.DayInterval(2));
	RecurringJob.AddOrUpdate("ExecuteScrapeSubscribedProductLevel1", () => signalService.ExecuteScrapeSubscribedProduct(1), Cron.DayInterval(4));

}



// create run time
Runtime.PythonDLL  = builder.Configuration["Scrapy:PythonDll"];
PythonEngine.Initialize();

app.Run();