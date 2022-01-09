using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.ViewModels;
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
            var product = _dbContext.ProductItems.FirstOrDefault(c => c.Id == id);
            if (product != null)
            {
                var relatedProducts = _dbContext.ProductRelations
                    .Where(p => p.ProductId == product.Id)
                    .Select(p => p.RelatedProduct)
                    .OrderByDescending(p => p.Price)
                    .ToList();
                var viewModel = new DetailViewModel(product, relatedProducts);

                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
