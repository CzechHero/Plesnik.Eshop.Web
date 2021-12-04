using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Database
{
    public class DatabaseInit
    {
        public void Initialize(EShopDbContext eShopDbContext)
        {
            eShopDbContext.Database.EnsureCreated();

            // Generate dummy carousel items and product items if the created tables are empty
            if (eShopDbContext.CarouselItems.Count() == 0)
            {
                IList<CarouselItem> carouselItems = GenerateCarouselItems();
                foreach(var ci in carouselItems)
                {
                    eShopDbContext.CarouselItems.Add(ci);
                }
            }

            if (eShopDbContext.ProductItems.Count() == 0)
            {
                IList<ProductItem> productItems = GenerateProductItems();
                foreach(var pi in productItems)
                {
                    eShopDbContext.ProductItems.Add(pi);
                }
            }

            eShopDbContext.SaveChanges();
        }

        public List<CarouselItem> GenerateCarouselItems()
        {
            List<CarouselItem> carouselItems = new List<CarouselItem>()
            {
                new CarouselItem("/img/carousel/image1.jpg", "Tech 1"),
                new CarouselItem("/img/carousel/image2.jpg", "Tech 2"),
                new CarouselItem("/img/carousel/image3.jpg", "Tech 3"),
                new CarouselItem("/img/carousel/image4.jpg", "Tech 4")
            };

            return carouselItems;
        }

        public List<ProductItem> GenerateProductItems()
        {
            List<ProductItem> productItems = new List<ProductItem>()
            {
                new ProductItem(
                    "Arduino DUE",
                    "Mini počítač AT91SAM3X8E, RAM 0,00GB, Flash 0,00 GB, Bez operačního systému",
                    1079,
                    "/img/product/product01.jpg",
                    "Arduino DUE"
                    ),
                new ProductItem(
                    "Arduino UNO Rev3",
                    "Mini počítač - Mikroprocesor Atmel ATMega328, Flash paměť 32 KB, 14 digitálních a 6 analogových pinů, rozhraní USB a SPI",
                    659,
                    "/img/product/product02.jpg",
                    "Arduino UNO Rev3"
                    )
            };

            return productItems;
        }

        public async Task EnsureRoleCreated(RoleManager<Role> roleManager)
        {
            string[] roles = Enum.GetNames(typeof(RolesEnum));

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new Role(role));
            }
        }

        public async Task EnsureAdminCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "admin",
                Email = "admin@admin.cz",
                EmailConfirmed = true,
                FirstName = "jmeno",
                LastName = "prijmeni"
            };
            string password = "abc";

            User adminInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (adminInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(RolesEnum));
                    foreach (var role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Admin: {error.Code}, {error.Description}");
                    }
                }
            }

        }

        public async Task EnsureManagerCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "manager",
                Email = "manager@manager.cz",
                EmailConfirmed = true,
                FirstName = "jmeno",
                LastName = "prijmeni"
            };
            string password = "abc";

            User managerInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (managerInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(RolesEnum));
                    foreach (var role in roles)
                    {
                        if (role != RolesEnum.Admin.ToString())
                            await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Manager: {error.Code}, {error.Description}");
                    }
                }
            }

        }
    }
}
