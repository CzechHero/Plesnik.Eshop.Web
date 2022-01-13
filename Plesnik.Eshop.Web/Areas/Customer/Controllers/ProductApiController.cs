using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(RolesEnum.Customer))]
    [Route("api/product")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly EShopDbContext _dbContext;

        public ProductApiController(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("related")]
        [Route("api/product/related")]
        [Produces("application/json")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> RelatedSearch(int productId, string query)
        {
            var product = await _dbContext.ProductItems.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
            {
                return BadRequest();
            }

            try
            {
                var relatedProducts = _dbContext.ProductRelations
                    .Where(p => p.ProductId == product.Id)
                    .Select(p => p.RelatedProduct)
                    .ToList();
                var result = _dbContext.ProductItems
                    .Where(p => p.Id != product.Id)
                    .AsEnumerable();
                var res = result
                    .Where(p => !relatedProducts.Any(p2 => p2.Id == p.Id))
                    .Where(p => p.Name.ToUpperInvariant().Contains(query.ToUpperInvariant()))
                    .Take(10)
                    .OrderBy(p => p.Id)
                    .Select(p => new
                    {
                        Label = p.Name,
                        Value = p
                    })
                    .ToList();

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
