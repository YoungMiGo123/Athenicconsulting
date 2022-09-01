using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core.ViewModels
{
    public class FilterByBrandViewModel
    {
        public IEnumerable<CampaignFolder> CampaignFolders { get;  set; }
        public Brand Brand { get;  set; }
    }
}
