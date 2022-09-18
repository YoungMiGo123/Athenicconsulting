using AthenicConsulting.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AthenicConsulting.Office.Views.Brands.ViewModels
{
    public class EditBrandViewModel
    {
        public Brand Brand { get; set; }
        public string Message { get; set; }
        public bool CanDisplayMessage => !string.IsNullOrWhiteSpace(Message);

        public List<SelectListItem> IndustryList { get; set; }
        public IFormFile Logo { get; set; }
        public string SelectedIndustry { get; set; }
    }
}
