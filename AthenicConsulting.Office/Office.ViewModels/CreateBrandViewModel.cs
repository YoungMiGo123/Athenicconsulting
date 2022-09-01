using AthenicConsulting.Core.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AthenicConsulting.Office.Office.ViewModels
{
    public class CreateBrandViewModel
    {
        public string Name { get; set; }
        public Industry Industry { get; set; }
        [Required(ErrorMessage = "Please select a image for a logo")]
        public IFormFile Logo { get; set; }
        public string Description { get; set; }

        public List<SelectListItem> IndustrySelectList { get; set; }
        public string SelectedIndustry { get; set; }
    }
}
