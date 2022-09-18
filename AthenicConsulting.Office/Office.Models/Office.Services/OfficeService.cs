using AthenicConsulting.Core;
using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.Utitlies;
using AthenicConsulting.Identity.Identity.Interfaces;
using AthenicConsulting.Office.Office.Interfaces;
using AthenicConsulting.Office.Office.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AthenicConsulting.Office.Office.Models.Office.Services
{
    public class OfficeService : IOfficeService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public IFileHelper FileHelper { get; set; }
        public ILeadService LeadService { get; set; }
        public IUserService UserService { get; set; }

        public OfficeService(IUnitOfWork unitOfWork, IFileHelper fileHelper, ILeadService leadService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            FileHelper = fileHelper;
            LeadService = leadService;
            UserService = userService;
        }

        public async Task<Brand> CreateBrand(CreateBrandViewModel createBrandViewModel)
        {
            var brandFileResult = FileHelper.CreateBrandFolder(createBrandViewModel.Name);
            var logoFileResult = FileHelper.UploadFile(@$"{brandFileResult.FileName}\Logo", createBrandViewModel.Logo);
            var brand = new Brand
            {
                Logo = logoFileResult.FileName.Trim(),
                CreatedDate = DateTime.UtcNow,
                Deactivatated = false,
                Description = createBrandViewModel.Description.Trim(),
                IndustryId = Convert.ToInt32(createBrandViewModel.SelectedIndustry),
                ModifiedDate = DateTime.UtcNow,
                Name = createBrandViewModel.Name.Trim(),
            };
            var brandResult = await _unitOfWork.BrandRepo.AddItem(brand);
            _unitOfWork.SaveChanges();
           return brandResult;
        }

        public bool DeleteBrand(Brand brand)
        {
            var softDeletedBrand = _unitOfWork.BrandRepo.SoftDeleteEntity(brand);
            _unitOfWork.SaveChanges();
            return softDeletedBrand != null;    
        }

        public Brand UpdateBrand(Brand brand)
        {
           var updatedBrand = _unitOfWork.BrandRepo.UpdateEntity(brand);
            _unitOfWork.SaveChanges();
            return updatedBrand;
        }

        public bool ArchiveBrand(Brand brand)
        {
            var archivedBrand = DeleteBrand(brand);
            _unitOfWork.SaveChanges();
            return archivedBrand;
        }

        public Brand GetBrand(int BrandId)
        {
            return _unitOfWork.BrandRepo.GetById<Brand>(BrandId);
        }

        public async  Task<Campaign> CreateCampaign(CreateCampaignViewModel campaignViewModel)
        {
            var brand = GetBrand(Convert.ToInt32(campaignViewModel.BrandId));
            var campaignFolder = FileHelper.CreateCampaignFolder(brand.Name, campaignViewModel.Name);
            FileHelper.UploadFiles(campaignFolder.FileName, campaignViewModel.FormFiles);
            var campaign = new Campaign
            {
                BrandId = brand.Id,
                CreatedDate = DateTime.UtcNow,
                ImgFolderPath = brand.Name,
                CampaignType = (CampaignType)Convert.ToInt32(campaignViewModel.CampaignType),
                ModifiedDate = DateTime.UtcNow,
                Name = campaignViewModel.Name,
            };
            var item = await _unitOfWork.CampaignRepo.AddItem(campaign);
            _unitOfWork.SaveChanges();
            return item;
        }

        public bool DeleteCampaign(Campaign campaign)
        {
            var softDeletedBrand = _unitOfWork.CampaignRepo.SoftDeleteEntity(campaign);
            _unitOfWork.SaveChanges();
            return softDeletedBrand != null;
        }

        public Campaign UpdateCampaign(Campaign campaign)
        {
            var item = _unitOfWork.CampaignRepo.UpdateEntity(campaign);
            _unitOfWork.SaveChanges();
            return item;
        }

        public bool ArchiveCampaign(Campaign campaign)
        {
            return DeleteCampaign(campaign);
        }

        public Campaign GetCampaign(int BrandId)
        {
            return _unitOfWork.CampaignRepo.GetById<Campaign>(BrandId);
        }

        public async Task<List<SelectListItem>> GetIndustriesListAsync()
        {
            var industries = await _unitOfWork.IndustryRepository.GetAllAsync<Industry>();
            var selectList = industries.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            }).ToList();
            return selectList;
        }

        public List<SelectListItem> GetCampaignTypes()
        {
            var campaignTypes = EnumUtilities.GetEnums<CampaignType>();
            var selectList = campaignTypes.Select(x => new SelectListItem
            {
                Text = x.GetDisplayName(),
                Value = $"{(int)x}"
            }).ToList();
            return selectList;
        }

        public async Task<List<SelectListItem>> GetBrandsTypesAsync()
        {
            var brands = await _unitOfWork.BrandRepo.GetAllAsync<Brand>();
            return brands.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            }).ToList();
        }

        public List<Campaign> GetCampaigns()
        {
            var campaigns = _unitOfWork.CampaignRepo.GetAll<Campaign>(includeProperties: "Brand");
            return campaigns.ToList();
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.BrandRepo.GetAllAsync<Brand>();
            return brands.ToList();
        }
    }
}
