using AthenicConsulting.Core;
using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.Utitlies;
using AthenicConsulting.Office.Office.Interfaces;
using AthenicConsulting.Seeder.Seeder.Interfaces;

namespace AthenicConsulting.Seeder.Seeder.Models
{
    public class SeederService : ISeederService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeederService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool SeedBrands(string path)
        {
            var brandsLines = File.ReadLines(path);
            var isHeader = true;
            var items = new List<Brand>();
            foreach (var brandLine in brandsLines)
            {
                if (isHeader) 
                { 
                    isHeader = false; 
                    continue;
                }
                var lines = brandLine.Split(",");
                if (lines.Length < 3) 
                { 
                    continue;
                }

                var brandName = lines[0];
                if (brandName.StartsWith("Snow") || brandName.StartsWith("CVG") || brandName.StartsWith("Alpha") || brandName.StartsWith("Figs")) 
                { 
                    continue; 
                }
                var brand = new Brand
                {
                    CreatedDate = DateTime.UtcNow,
                    Deactivatated = false,
                    Description = lines[2],
                    Industry = _unitOfWork.IndustryRepository.GetFirstOrDefault<Industry>(x => x.Name.Contains(lines[1])),
                    Name = brandName,
                    Logo = GetLogo(lines[0]),
                    ModifiedDate = DateTime.UtcNow
                };
                items.Add(brand);
            }
            _unitOfWork.BrandRepo.AddItems(items);
            return _unitOfWork.SaveChanges();
        }

        public async Task<bool> SeedCampaigns()
        {
            var brands = await _unitOfWork.BrandRepo.GetAllAsync<Brand>();
            foreach (var brand in brands)
            {
                var campaigns = GetAllCampaignTypes(brand);
                await _unitOfWork.CampaignRepo.AddItems(campaigns);
            }
            return _unitOfWork.SaveChanges();
        }

        private string GetLogo(string brandName)
        {
            var path = @$"C:\Users\ACER\source\repos\AthenicConsult\AthenicConsult\wwwroot\marketdata\Brands\{brandName}\Logo";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var files = Directory.GetFiles(path)?.FirstOrDefault() ?? string.Empty;
            var logo = files.Split(@"\").LastOrDefault() ?? string.Empty;
            return logo;
        }
        private List<Campaign> GetAllCampaignTypes(Brand brand)
        {
            var campaignTypes = EnumUtilities.GetEnums<CampaignType>();
            var campaigns = new List<Campaign>();
            foreach (var campaignType in campaignTypes)
            {
                campaigns.Add(new Campaign
                {
                    Brand = brand,
                    CampaignType = campaignType,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    ImgFolderPath = brand.Name,
                    Name = $"{campaignType}",
                    Deactivatated = false,
                });
            }
            return campaigns;
        }
    }
}
