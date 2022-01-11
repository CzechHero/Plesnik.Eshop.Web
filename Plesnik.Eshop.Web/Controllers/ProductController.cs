using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.Identity;
using Plesnik.Eshop.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost]
        [Authorize(Roles = nameof(RolesEnum.Admin) + ", " + nameof(RolesEnum.Manager) + ", " + nameof(RolesEnum.Customer))]
        public async Task<IActionResult> Suggest(int productId, int relatedId)
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

            // Get current user
            ClaimsPrincipal currentUser = User;
            string currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            int id;
            bool idOk = int.TryParse(currentUserId, out id);
            if (!idOk)
            {
                return BadRequest();
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Check if already suggested by current user
            var existingSuggestion = _dbContext.ProductRelationEntries
                .FirstOrDefault(e => e.UserId == user.Id && e.ProductId == productId && e.RelatedProductId == relatedId);
            if (existingSuggestion != null)
            {
                return BadRequest();
            }

            // Add new suggestion
            var newSuggestion = new ProductRelationEntry(
                product.Id, relatedProduct.Id, user.Id, product, relatedProduct, user
            );
            await _dbContext.ProductRelationEntries.AddAsync(newSuggestion);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(ProductController.Detail), new { id = productId });
        }
    }
}
