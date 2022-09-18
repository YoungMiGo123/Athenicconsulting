using AthenicConsulting.Office.Office.Interfaces;
using AthenicConsulting.Office.Office.ViewModels;
using AthenicConsulting.Office.Views.Brands.ViewModels;
using AthenicConsulting.Office.Views.Campaigns.ViewModels;
using AthenicConsulting.Office.Views.ViewModels;
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
        public async Task<IActionResult> CreateBrandAsync(CreateBrandViewModel createBrandViewModel)
        {
            var brand = await _officeService.CreateBrand(createBrandViewModel);
            if (brand != null)
            {
                return RedirectToAction("Index", new DashboardViewModel { Message = "The brand was successfully created" });
            }
            return View(createBrandViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCampaign()
        {
            var vm = new CreateCampaignViewModel()
            {
                CampaignTypes = _officeService.GetCampaignTypes(),
                Brands = await _officeService.GetBrandsTypesAsync(),
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCampaignAsync(CreateCampaignViewModel createCampaignViewModel)
        {
            var campaign = await _officeService.CreateCampaign(createCampaignViewModel);
            if (campaign != null)
            {
                return RedirectToAction("Index", new DashboardViewModel { Message = "The campaign was successfully created" });
            }
            return View(createCampaignViewModel);
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
            var vm = new ManageBrandsViewModel
            {
                Brands = _officeService.GetBrands()
            };
            return View(vm);
        }
        public IActionResult ManageCampaigns()
        {
            var vm = new ManageCampaignsViewModel
            {
                Campaigns = _officeService.GetCampaigns()
            };
            return View(vm);
        }
        public async Task<IActionResult> EditBrand(int brandId)
        {
            var industryList = await _officeService.GetIndustriesListAsync();
            var brand = _officeService.GetBrand(brandId);
            var vm = new EditBrandViewModel
            {
                Brand = brand,
                IndustryList = industryList
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult EditBrand(EditBrandViewModel editBrandViewModel)
        {
            var updatedBrand = _officeService.UpdateBrand(editBrandViewModel);
            if (updatedBrand != null)
            {
                return RedirectToAction("Index", new DashboardViewModel { Message = "The brand was successfully updated" });
            }
            return View();
        }
        public IActionResult DelteBrand(int brandId)
        {
            var brand = _officeService.GetBrand(brandId);
            var vm = new DeleteBrandViewModel
            {
                Brand = brand
            };
            return View(vm);

        }
        [HttpPost]
        public IActionResult DeleteBrand(DeleteBrandViewModel deleteBrandViewModel)
        {
            if (deleteBrandViewModel.ConfirmDelete)
            {
                var isDeleted = _officeService.DeleteBrand(deleteBrandViewModel.Brand);
                if (isDeleted)
                {
                     return RedirectToAction("Index", new DashboardViewModel { Message = "The brand was successfully deleted" });
                }
            }
            return View(deleteBrandViewModel);
        }
        public IActionResult EditCampaign(int campaignId)
        {
            var campaign = _officeService.GetCampaign(campaignId);
            var vm = new EditCampaignViewModel()
            {
                Campaign = campaign
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult EditCampaign(EditCampaignViewModel editCampaignViewModel)
        {
            return View();
        }
    }
}
