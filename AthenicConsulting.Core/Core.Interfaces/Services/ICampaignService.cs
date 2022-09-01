using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core.Core.Interfaces.Services
{
    public interface ICampaignService
    {
        IEnumerable<CampaignFolder> GetCampaignFolders(IEnumerable<Campaign> campaigns);
    }
}
