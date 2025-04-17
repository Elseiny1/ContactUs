using System.Diagnostics;
using System.Threading.Tasks;
using Feedbacks.Repos.ServiceManagement;
using Feedbacks.ViewModels.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace Feedbacks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IServiceRepo _serviceRepo;

        public HomeController(ILogger<HomeController> logger,
            IServiceRepo serviceRepo)
        {
            _logger = logger;
            _serviceRepo = serviceRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }  

        [HttpPost]
        public async Task<IActionResult> Index(ServiceViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _serviceRepo.AddServiceAsync(model);
            if(result is null)
            {
                model.Massage = "couldn't add the service";
                return View(model);
            }
            model.Massage = "service added successfully";
            return View(model);
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
