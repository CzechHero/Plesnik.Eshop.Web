using Microsoft.AspNetCore.Mvc;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IActionResult Select()
        {
            IList<ProductItem> productItems = DatabaseFake.ProductItems;

            return View(productItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductItem productItem)
        {
            if (productItem != null && !string.IsNullOrEmpty(productItem.Name))
            {
                if (DatabaseFake.ProductItems != null && DatabaseFake.ProductItems.Count > 0)
                {
                    productItem.Id = DatabaseFake.ProductItems.Last().Id + 1;
                }

                DatabaseFake.ProductItems.Add(productItem);
                return RedirectToAction(nameof(ProductController.Select));
            }
            else
            {
                return View(productItem);
            }
        }

        public IActionResult Edit(int id)
        {
            var foundItem = DatabaseFake.ProductItems.FirstOrDefault(c => c.Id == id);
            if (foundItem != null)
            {
                return View(foundItem);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductItem productItemEdit)
        {
            var foundItem = DatabaseFake.ProductItems.FirstOrDefault(c => c.Id == productItemEdit.Id);
            if (foundItem != null)
            {
                foundItem.Name = productItemEdit.Name;
                return RedirectToAction(nameof(ProductController.Select));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Delete(int id)
        {
            IList<ProductItem> productItems = DatabaseFake.ProductItems;
            var productItem = productItems.FirstOrDefault(i => i.Id == id);
            if (productItem != null)
                productItems.Remove(productItem);

            return RedirectToAction(nameof(ProductController.Select));
        }
    }
}
