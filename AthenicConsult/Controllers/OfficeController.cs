using AthenicConsulting.Office.Office.Interfaces;
using AthenicConsulting.Office.Office.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthenicConsulting.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OfficeController : Controller
    {
        private IOfficeService _officeService;
        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }
       
        [HttpGet]
        public IActionResult Index(DashboardViewModel? dashboardViewModel = null)
        {
            if (!_officeService.UserService.isUserSignedIn(User)) { return RedirectToAction("Login", "Account"); }

            if (dashboardViewModel == null)
            {
                dashboardViewModel = new DashboardViewModel() { Message = string.Empty };
            }
            return View(dashboardViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> CreateBrand()
        {
            var industryList = await _officeService.GetIndustriesListAsync();
            var vm = new CreateBrandViewModel()
            {
                IndustrySelectList = industryList
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateBrand(CreateBrandViewModel createBrandViewModel)
        {
            _officeService.CreateBrand(createBrandViewModel);
            return RedirectToAction("Index", new DashboardViewModel { Message = "The brand was successfully created" });
        }

        [HttpGet]
        public async Task<IActionResult> CreateCampaign()
        {
            var vm = new CreateCampaignViewModel()
            {
                CampaignTypes = _officeService.GetCampaignTypes(),
                Brands = await _officeService.GetBrandsAsync(),
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateCampaign(CreateCampaignViewModel createCampaignViewModel)
        {
            _officeService.CreateCampaign(createCampaignViewModel);
            return RedirectToAction("Index", new DashboardViewModel { Message = "The brand was successfully created" });
        }
        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            var vm = new ReportsViewModel
            {
                Leads = await _officeService.LeadService.GetLeads()
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult ManageBrands()
        {
            return View();
        }
        public IActionResult ManageCampaigns()
        {
            return View();
        }
    
    }
}
