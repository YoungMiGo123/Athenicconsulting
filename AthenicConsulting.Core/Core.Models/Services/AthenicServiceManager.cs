using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.Entity;
using AthenicConsulting.Core.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace AthenicConsulting.Core.Services
{
    public class AthenicServiceManager : IAthenicServiceManager
    {
        private readonly IBrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampaignService _campaignService;
        private string BaseLogoPath;
        public AthenicServiceManager(IBrandService brandService, IUnitOfWork unitOfWork, ICampaignService campaignService)
        {
            _brandService = brandService;
            _unitOfWork = unitOfWork;
            _campaignService = campaignService;
            BaseLogoPath = @$"/marketdata/Brands";
        }

        public async Task<Lead> AddLeadAsync(LeadViewModel leadViewModel)
        {
            var lead = await _unitOfWork.LeadRepository.AddItem(new Lead
            {
                CreatedDate = DateTime.UtcNow,
                Deactivatated = false,
                Email = leadViewModel.Email,
                Subscribed = true,
                ModifiedDate = DateTime.UtcNow
            });
            _unitOfWork.SaveChanges();
            return lead;
        }

        public BrandViewModel GetBrandViewModel()
        {
            return new BrandViewModel
            {
                BaseLogoPath = BaseLogoPath,
                Brands = _unitOfWork.BrandRepo.GetAll<Brand>(100).ToList()
            };
        }

        public FilterByBrandViewModel GetFilterByBrandViewModel(int brandId)
        {
            var brand = _brandService.GetBrandById(brandId);
            var campaignFolders = _brandService.GetBrandImages(brand.Id);
            return new FilterByBrandViewModel
            {
                CampaignFolders = campaignFolders,
                Brand = brand
            };
        }

        public FilterByCampaignViewModel GetFilterByCampaignViewModel(CampaignType campaignType)
        {
            var campaigns = _unitOfWork.CampaignRepo.GetAll<Campaign>(filter: x => x.CampaignType == campaignType,includeProperties: "Brand") ?? new List<Campaign>();
            var campaignFolders = _campaignService.GetCampaignFolders(campaigns);
            return new FilterByCampaignViewModel
            {
                CampaignFolders = campaignFolders,
                Campaign = _unitOfWork.CampaignRepo.GetFirstOrDefault<Campaign>(filter: x => x.CampaignType == campaignType)
            };
        }

        public FilterByIndustryViewModel GetFilterByIndustryViewModel(int industryId)
        {
            var brands = _unitOfWork.BrandRepo.GetAll<Brand>(filter: x => x.IndustryId == industryId)?.ToList() ?? new List<Brand>();
            var brandView = new BrandViewModel
            {
                BaseLogoPath = BaseLogoPath,
                Brands = brands
            };
            return new FilterByIndustryViewModel
            {
                BrandViewModel = brandView,
                Industry = _unitOfWork.IndustryRepository.GetById<Industry>(industryId)
            };
        }

        public IndustryViewModel GetIndustryViewModel()
        {
            return new IndustryViewModel
            {
                Industries = _unitOfWork.IndustryRepository.GetAll<Industry>(25).ToList(),
            };
        }

        public LandingPageViewModel GetLandingPage()
        {
            var brands = _brandService.GetAllBrands(8);
            var limitPerBrand = 1;
           
            var campaignFolders = new List<CampaignFolder>();
            foreach (var brand in brands)
            {
                var campaignFolderList = _brandService.GetBrandImages(brand.Id);
                var limitedCampaignFolders = GetCampaignFoldersWithLimit(limitPerBrand, campaignFolderList.Where(x => x.Images.Any() && x.CampaignType != CampaignType.PopUp));
                campaignFolders.AddRange(limitedCampaignFolders);
            }
            return new LandingPageViewModel
            {
                CampaignFolders = campaignFolders,
                Industries = _unitOfWork.IndustryRepository.GetAll<Industry>(25)
            };
        }
        private IEnumerable<CampaignFolder> GetCampaignFoldersWithLimit(int limit, IEnumerable<CampaignFolder> campaignFolders)
        {
            var random = new Random();
            var list = new List<CampaignFolder>();
            var maxChecks = 0;
            for(int i = 0; i < limit; i++)
            {
                var indexOfItem = random.Next(0, campaignFolders.Count());
                var item = campaignFolders.ElementAtOrDefault(indexOfItem);
                if(item == null) { continue; }
                do
                {
                    indexOfItem = random.Next(0, campaignFolders.Count());
                    item = campaignFolders.ElementAt(indexOfItem);
                    if (!list.Any(x => x.CampaignId == item.CampaignId))
                    {
                        item.Images = item?.Images?.Take(limit)?.ToList() ?? new List<string>();
                        list.Add(item);
                        break;
                    }
                    if (maxChecks >= 5) { break; }
                    maxChecks++;
                }
                while (list.Any(x => x.CampaignId == item.CampaignId));
                maxChecks = 0;
               
            } 
            return list; 
        }
    }
}
