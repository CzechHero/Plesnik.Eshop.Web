using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plesnik.Eshop.Web.Models.ApplicationServices.Abstraction;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.Identity;

namespace Plesnik.Eshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(RolesEnum.Customer))]
    public class CustomerOrdersController : Controller
    {

        ISecurityApplicationService iSecure;
        EShopDbContext EshopDbContext;

        public CustomerOrdersController(ISecurityApplicationService iSecure, EShopDbContext eshopDBContext)
        {
            this.iSecure = iSecure;
            EshopDbContext = eshopDBContext;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User currentUser = await iSecure.GetCurrentUser(User);
                if (currentUser != null)
                {
                    IList<Order> userOrders = await this.EshopDbContext.Orders
                        .Where(or => or.UserId == currentUser.Id)
                        .Include(o => o.User)
                        .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .ToListAsync();
                    return View(userOrders);
                }
            }

            return NotFound();
        }
    }
}
