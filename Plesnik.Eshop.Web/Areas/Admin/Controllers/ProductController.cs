using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.Identity;
using Plesnik.Eshop.Web.Models.Implementation;
using Plesnik.Eshop.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(RolesEnum.Admin) + ", " + nameof(RolesEnum.Manager))]
    public class ProductController : Controller
    {
        private readonly EShopDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public ProductController(EShopDbContext eShopDbContext, IWebHostEnvironment env)
        {
            _dbContext = eShopDbContext;
            _env = env;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _dbContext.ProductItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var relatedProducts = _dbContext.ProductRelations
                .Where(p => p.ProductId == product.Id)
                .Select(p => p.RelatedProduct)
                .OrderByDescending(p => p.Price)
                .ToList();
            var viewModel = new DetailViewModel(product, relatedProducts);

            return View(viewModel);
        }

        public IActionResult Select()
        {
            IList<ProductItem> productItems = _dbContext.ProductItems.ToList();

            return View(productItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductItem productItem)
        {
            if (productItem != null && productItem.Image != null)
            {
                FileUpload fileUpload = new FileUpload(_env.WebRootPath, "img/product", "image");
                productItem.ImageSource = await fileUpload.FileUploadAsync(productItem.Image);

                if (!string.IsNullOrWhiteSpace(productItem.ImageSource))
                {
                    _dbContext.ProductItems.Add(productItem);

                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(ProductController.Select));
                }
            }

            return View(productItem);
        }

        public IActionResult Edit(int id)
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

        [HttpPost]
        public async Task<IActionResult> Edit(ProductItem productItem)
        {
            var foundItem = _dbContext.ProductItems.FirstOrDefault(c => c.Id == productItem.Id);
            if (foundItem != null)
            {
                if (productItem != null && productItem.Image != null)
                {
                    FileUpload fileUpload = new FileUpload(_env.WebRootPath, "img/product", "image");
                    productItem.ImageSource = await fileUpload.FileUploadAsync(productItem.Image);

                    if (!string.IsNullOrWhiteSpace(productItem.ImageSource))
                    {
                        foundItem.ImageSource = productItem.ImageSource;
                    }
                }

                foundItem.Name = productItem.Name;
                foundItem.Description = productItem.Description;
                foundItem.Price = productItem.Price;
                foundItem.ImageAlt = productItem.ImageAlt;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(ProductController.Select));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            DbSet<ProductItem> carouselItems = _dbContext.ProductItems;
            var productItem = carouselItems.FirstOrDefault(i => i.Id == id);
            if (productItem != null)
            {
                carouselItems.Remove(productItem);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ProductController.Select));
        }

        public async Task<IActionResult> DeleteRelated(int relatedId, int productId)
        {
            var relatedItem = await _dbContext.ProductRelations
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.RelatedProductId == relatedId);
            if (relatedItem == null)
            {
                return NotFound();
            }

            _dbContext.ProductRelations.Remove(relatedItem);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(this.Details), new { id = productId });
        }

        [HttpPost]
        public async Task<IActionResult> AddRelated(int productId, int relatedId)
        {
            // Validate
            var product = _dbContext.ProductItems.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            var relatedProduct = _dbContext.ProductItems.FirstOrDefault(p => p.Id == relatedId);
            if (relatedProduct == null)
            {
                return NotFound();
            }

            // Check if relation exists
            if (_dbContext.ProductRelations.Any(p => p.ProductId == productId && p.RelatedProductId == relatedId))
            {
                return BadRequest();
            }

            // Add new related item
            var newRelatedItem = new ProductRelation(
                product.Id, relatedProduct.Id, product, relatedProduct
            );
            await _dbContext.ProductRelations.AddAsync(newRelatedItem);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(this.Details), new { id = productId });
        }
    }
}
