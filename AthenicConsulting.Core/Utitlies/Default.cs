using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenicConsulting.Core.Utitlies
{
    public static class Default
    {
        public static List<Brand> GetDefaultBrands()
        {
            var brands = new List<Brand>()
            {
                new Brand
                {
                    CreatedDate = DateTime.UtcNow,
                    Description = "Shop Constantly Varied Gear's selection of squat approved workout leggings, funny fitness shirts, unique sports bras, and more",
                    Id = 1,
                    ModifiedDate = DateTime.UtcNow,
                    Logo = "",
                    Name = "Constantly Varied Gear"
                },
                new Brand
                {
                    Id = 2,
                    CreatedDate = DateTime.UtcNow,
                    Description = "",
                    ModifiedDate= DateTime.UtcNow,
                    Name = "Snow Oral Care",
                    Logo = ""
                }
            };
            return brands;
        }


        public static List<Campaign> GetCampaigns(List<Brand> brands)
        {
            var basePath = @$"{Directory.GetCurrentDirectory()}\wwwroot\marketdata\Brands";
            var directories = Directory.GetDirectories(basePath);
            var campaigns = new List<Campaign>();
            var index = 0;
            var rnd = new Random();
            var totalCompaignTypes = 8;
            foreach (var brandDirectory in directories)
            {
                for (var i = 0; i < totalCompaignTypes; i++)
                {
                    var campaignType = GetCampaignType(i);
                    campaigns.Add(new Campaign
                    {
                        Brand = brands.ElementAt(index),
                        BrandId = brands.ElementAt(index).Id,
                        CampaignType = campaignType,
                        CreatedDate = DateTime.UtcNow,
                        Id = rnd.Next(1, 5000),
                        ImgFolderPath = brandDirectory.Split(@"\").Last(),
                        ModifiedDate = DateTime.UtcNow,
                        Name = $"{campaignType}"
                    });
                }
                index++;
            }
            return campaigns;
        }
        public static CampaignType GetCampaignType(int Id)
        {
            switch (Id)
            {
                case 0: return CampaignType.Abandoned;
                case 1: return CampaignType.Offer;
                case 2: return CampaignType.ProductLaunch;
                case 3: return CampaignType.Unengaged;
                case 4: return CampaignType.PostPurchase;
                case 5: return CampaignType.Welcome;
                case 6: return CampaignType.Holidays;
                case 7: return CampaignType.CustomerWinBack;
                case 9: return CampaignType.BackInStock;
                default: return CampaignType.Other;
            }
        }
    }
}
