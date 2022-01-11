using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(RolesEnum.Admin) + ", " + nameof(RolesEnum.Manager))]
    public class ProductRelationEntryController : Controller
    {
        private readonly EShopDbContext _dbContext;

        public ProductRelationEntryController(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Select()
        {
            var suggestionList = await _dbContext.ProductRelationEntries
                .Include(e => e.Product)
                .Include(e => e.RelatedProduct)
                .Include(e => e.User)
                .ToListAsync();

            return View(suggestionList);
        }

        public async Task<IActionResult> Approve(int? id)
        {
            var relatedEntry = await _dbContext.ProductRelationEntries
                .Include(e => e.Product)
                .Include(e => e.RelatedProduct)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (relatedEntry == null)
            {
                return BadRequest();
            }

            // Create new related product
            var relatedProduct = new ProductRelation(
                relatedEntry.ProductId,
                relatedEntry.RelatedProductId,
                relatedEntry.Product,
                relatedEntry.RelatedProduct
            );

            _dbContext.ProductRelations.Add(relatedProduct);

            // Delete entries from other users
            _dbContext.ProductRelationEntries
                .RemoveRange(_dbContext.ProductRelationEntries
                    .Where(e => e.ProductId == relatedEntry.ProductId && e.RelatedProductId == relatedEntry.RelatedProductId));

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(ProductRelationEntryController.Select));
        }

        public async Task<IActionResult> Disapprove(int? id)
        {
            var relatedEntry = await _dbContext.ProductRelationEntries
                .FirstOrDefaultAsync(e => e.Id == id);
            if (relatedEntry == null)
            {
                return BadRequest();
            }

            _dbContext.ProductRelationEntries.Remove(relatedEntry);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(ProductRelationEntryController.Select));
        }
    }
}
