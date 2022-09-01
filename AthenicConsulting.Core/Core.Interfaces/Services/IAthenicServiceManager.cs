using AthenicConsulting.Core.Entity;
using AthenicConsulting.Core.ViewModels;

namespace AthenicConsulting.Core.Core.Interfaces.Services
{
    public interface IAthenicServiceManager
    {
        LandingPageViewModel GetLandingPage();
        FilterByCampaignViewModel GetFilterByCampaignViewModel(CampaignType campaignType);
        FilterByBrandViewModel GetFilterByBrandViewModel(int brandId);
        FilterByIndustryViewModel GetFilterByIndustryViewModel(int industryId);
        IndustryViewModel GetIndustryViewModel();
        BrandViewModel GetBrandViewModel();
        Task<Lead> AddLeadAsync(LeadViewModel leadViewModel);
    }
}
