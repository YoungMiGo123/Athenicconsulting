using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core.ViewModels
{
    public class LandingPageViewModel
    {
        public List<CampaignFolder> CampaignFolders { get; set; }
        public IEnumerable<Industry> Industries { get; internal set; }
    }
}
