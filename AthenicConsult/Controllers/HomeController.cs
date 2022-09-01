using AthenicConsult.Models;
using AthenicConsulting.Core;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AthenicConsult.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAthenicServiceManager _athenicServiceManager;

        public HomeController(ILogger<HomeController> logger, IAthenicServiceManager athenicServiceManager)
        {
            _logger = logger;
            _athenicServiceManager = athenicServiceManager;
        }

        public IActionResult Index()
        {
            try
            {

                var vm = _athenicServiceManager.GetLandingPage();
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error happened in the progress of getting the home view, {ex}");
                return RedirectToAction("Error");
            }

        }
        public IActionResult FilterByCampaign(CampaignType campaignType)
        {
            try
            {
                var vm = _athenicServiceManager.GetFilterByCampaignViewModel(campaignType);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error happened in the progress of getting the filter by campaign view, {ex}");
                return RedirectToAction("Error");
            }

        }
        public IActionResult FilterByIndustry(int industryId)
        {
            try
            {
                var vm = _athenicServiceManager.GetFilterByIndustryViewModel(industryId);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error happened in the progress of getting the filter by industry view, {ex}");
                return RedirectToAction("Error");
            }

        }
        public IActionResult FilterByBrand(int brandId)
        {
            try
            {
                var vm = _athenicServiceManager.GetFilterByBrandViewModel(brandId);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error happened in the progress of getting the filter by brand view, {ex}");
                return RedirectToAction("Error");
            }

        }
        public IActionResult Industry()
        {
            try
            {
                var vm = _athenicServiceManager.GetIndustryViewModel();
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error happened in the progress of getting the industry view, {ex}");
                return RedirectToAction("Error");
            }

        }
        public IActionResult Brands()
        {
            try
            {
                var vm = _athenicServiceManager.GetBrandViewModel();
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error happened in the progress of getting the brand view, {ex}");
                return RedirectToAction("Error");
            }
        }
        public IActionResult AddLead(LeadViewModel leadViewModel)
        {
            try
            {
                _athenicServiceManager.AddLeadAsync(leadViewModel);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error happened in the progress of adding the lead, {ex}");
                return RedirectToAction("Error");
            }
     
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}