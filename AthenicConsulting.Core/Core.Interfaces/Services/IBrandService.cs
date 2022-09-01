using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core.Core.Interfaces.Services
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAllBrands(int count = 50);
        Brand GetBrandById(int id);
        IEnumerable<CampaignFolder> GetBrandImages(int id);
    }
}
