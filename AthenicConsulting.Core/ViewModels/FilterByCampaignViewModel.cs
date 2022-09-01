using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core.ViewModels
{
    public class FilterByCampaignViewModel
    {
        public IEnumerable<CampaignFolder> CampaignFolders { get; set; }
        public Campaign Campaign { get; set; }
    }
}
