using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.Utitlies;
using Microsoft.AspNetCore.Hosting;

namespace AthenicConsulting.Core.Core.Models.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public CampaignService(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public IEnumerable<CampaignFolder> GetCampaignFolders(IEnumerable<Campaign> campaigns)
        {
            var campaignFolderMap = new List<CampaignFolder>();
            
            foreach (var campaignValue in campaigns)
            {
                var path = @$"{_hostEnvironment.WebRootPath}\marketdata\Brands\{campaignValue.ImgFolderPath}\Campaigns\{campaignValue.Name}";
                if (Directory.Exists(path))
                {
                    var directories = Directory.GetFiles(path)?.Select(x => x.Replace(_hostEnvironment.WebRootPath, "")).Distinct();
                    campaignFolderMap.Add(new CampaignFolder
                    {
                        BrandId = campaignValue.BrandId,
                        BrandName = campaignValue.Brand.Name,
                        CampaignId = campaignValue.Id,
                        CampaignType = campaignValue.CampaignType,
                        Images = directories?.ToList() ?? new List<string>()
                    });

                }
            }
            return campaignFolderMap;
        }
    }
}
