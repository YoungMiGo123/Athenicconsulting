using AthenicConsulting.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AthenicConsulting.Office.Office.ViewModels
{
    public class CreateCampaignViewModel
    {
        public string CampaignType { get; set; }
        public string Name { get; set; }
        public string BrandId { get; set; }
        public IEnumerable<IFormFile> FormFiles { get; set; }
        public List<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> CampaignTypes { get; set; }
    }
}
