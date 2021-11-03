using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plesnik.Eshop.Web.Models;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EShopDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, EShopDbContext eShopDbContext)
        {
            _logger = logger;
            _dbContext = eShopDbContext;
        }

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.CarouselItems = _dbContext.CarouselItems.ToList();
            indexViewModel.ProductItems = _dbContext.ProductItems.ToList();

            return View(indexViewModel);
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
