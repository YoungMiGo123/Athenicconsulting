// See https://aka.ms/new-console-template for more information

using Athenic.Notifications.Notifications.Interfaces;
using Athenic.Notifications.Notifications.Models.Notifications.Services;
using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Core.Interfaces.Repositories;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Core.Core.Interfaces.Settings;
using AthenicConsulting.Core.Core.Models;
using AthenicConsulting.Core.Core.Models.Repositories;
using AthenicConsulting.Core.Core.Models.Services;
using AthenicConsulting.Core.Data.Contexts;
using AthenicConsulting.Core.Services;
using AthenicConsulting.Identity.Identity.Interfaces;
using AthenicConsulting.Identity.Identity.Models.Identity.Services;
using AthenicConsulting.Office.Office.Interfaces;
using AthenicConsulting.Office.Office.Models.Office.Services;
using AthenicConsulting.Seeder.Seeder.Interfaces;
using AthenicConsulting.Seeder.Seeder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var path = @"C:\Users\ACER\source\repos\AthenicConsult\AthenicConsult";

var configuration = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json", false)
		.Build();
var athenicSettings = configuration.GetSection("AthenicSettings")
    .Get<AthenicSettings>();

var Services = new ServiceCollection();

Services.AddSingleton<IAthenicSettings>(x => athenicSettings);

Services.AddDbContext<AthenicConsultingContext>(options =>
	 options.UseSqlServer(athenicSettings.ConnectionString, b => b.MigrationsAssembly("AthenicConsulting")));

Services.AddLogging(configure => configure.AddConsole());
Services.AddTransient<ICampaignRepository, CampaignRepository>();
Services.AddTransient<IBrandRepository, BrandRepository>();
Services.AddTransient<IIndustryRepository, IndustryRepository>();
Services.AddTransient<ILeadRepository, LeadRepository>();
Services.AddTransient<IUserRepository, UserRepository>();
Services.AddTransient<IUnitOfWork, UnitOfWork>();
Services.AddTransient<ILeadService, LeadService>();
Services.AddTransient<INotificationService, NotificationService>();
Services.AddTransient<ICampaignService, CampaignService>();
Services.AddTransient<IBrandService, BrandService>();
Services.AddTransient<IAthenicServiceManager, AthenicServiceManager>();
Services.AddTransient<IFileHelper, FileHelper>();
Services.AddTransient<IOfficeService, OfficeService>();
Services.AddTransient<IUserService, UserService>();
Services.AddTransient<ISeederService, SeederService>();

var builder = Services.BuildServiceProvider();
try
{
	var seederService = builder.GetService<ISeederService>();
	var basePath = Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net6.0", "");
	var csvPath = @$"{basePath}\BrandInfo.csv";
	var seeded = true; //seederService.SeedBrands(csvPath);
	if (seeded)
	{
		await seederService.SeedCampaigns();
	}
}
catch (Exception ex)
{

}

