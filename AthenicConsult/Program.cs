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
using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.Services;
using AthenicConsulting.Identity.Identity.Interfaces;
using AthenicConsulting.Identity.Identity.Models.Identity.Services;
using AthenicConsulting.Office.Office.Interfaces;
using AthenicConsulting.Office.Office.Models.Office.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)
        .Build();
var athenicSettings = configuration.GetSection("AthenicSettings")
    .Get<AthenicSettings>();
builder.Services.AddAuthentication();
builder.Services.AddSingleton<IAthenicSettings>(x => athenicSettings);
builder.Services.AddDbContext<AthenicConsultingContext>(options => options.UseSqlServer(athenicSettings.ConnectionString, b => b.MigrationsAssembly("AthenicConsulting")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AthenicConsultingContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDataProtection();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddLogging(configure => configure.AddConsole());
builder.Services.AddTransient<ICampaignRepository, CampaignRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IIndustryRepository, IndustryRepository>();
builder.Services.AddTransient<ILeadRepository, LeadRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ILeadService, LeadService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<ICampaignService, CampaignService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<IAthenicServiceManager, AthenicServiceManager>();
builder.Services.AddTransient<IFileHelper, FileHelper>();
builder.Services.AddTransient<IOfficeService, OfficeService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IUserService, UserService>();



var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
