using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core.Core.Models.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampaignService _campaignService;

        public BrandService(IUnitOfWork athenicRepo, ICampaignService campaignService)
        {
            _unitOfWork = athenicRepo;
            _campaignService = campaignService;
        }

        public IEnumerable<CampaignFolder> GetBrandImages(int id)
        {
            var brand = GetBrandById(id);
            var campaigns = _unitOfWork.CampaignRepo.GetAll<Campaign>(filter: q => q.BrandId == id);
            var campaignFolderMap = _campaignService.GetCampaignFolders(campaigns);
            return campaignFolderMap;
        }

        public IEnumerable<Brand> GetAllBrands(int count = 50)
        {
            return _unitOfWork.BrandRepo.GetAll<Brand>(count);
        }

        public Brand GetBrandById(int id)
        {
            return _unitOfWork.BrandRepo.GetById<Brand>(id);
        }

    }
}
