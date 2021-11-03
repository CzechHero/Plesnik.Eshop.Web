using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Plesnik.Eshop.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly EShopDbContext _dbContext;

        public ProductController(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Detail(int id)
        {
            var foundItem = _dbContext.ProductItems.FirstOrDefault(c => c.Id == id);
            if (foundItem != null)
            {
                return View(foundItem);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
