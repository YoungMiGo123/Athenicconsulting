using AthenicConsulting.Core;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Identity.Identity.Interfaces;
using AthenicConsulting.Office.Office.ViewModels;
using AthenicConsulting.Office.Views.Brands.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AthenicConsulting.Office.Office.Interfaces
{
    public interface IOfficeService
    {
        public Task<Brand> CreateBrand(CreateBrandViewModel brand);
        public bool DeleteBrand(Brand brand);
        public Brand UpdateBrand(EditBrandViewModel editBrandViewModel);
        public bool ArchiveBrand(Brand brand);
        public Brand GetBrand(int BrandId);
        public Task<List<SelectListItem>> GetBrandsTypesAsync();
        public Task<Campaign> CreateCampaign(CreateCampaignViewModel campaign);
        public bool DeleteCampaign(Campaign campaign);
        public Campaign UpdateCampaign(Campaign campaign);
        public bool ArchiveCampaign(Campaign campaign);
        public Campaign GetCampaign(int BrandId);
        public Task<List<SelectListItem>> GetIndustriesListAsync();
        public List<SelectListItem> GetCampaignTypes();
        public List<Campaign> GetCampaigns();
        public List<Brand> GetBrands();
        public IFileHelper FileHelper { get; set; }
        public ILeadService LeadService { get; set; }
        public IUserService UserService { get; set; } 
    }
}
