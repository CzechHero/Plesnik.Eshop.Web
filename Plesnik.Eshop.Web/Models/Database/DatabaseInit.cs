using Plesnik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Database
{
    public class DatabaseInit
    {
        public void Initialize(EShopDbContext eShopDbContext)
        {
            eShopDbContext.Database.EnsureCreated();

            if (eShopDbContext.CarouselItems.Count() == 0)
            {
                IList<CarouselItem> carouselItems = GenerateCarouselItems();
                foreach(var ci in carouselItems)
                {
                    eShopDbContext.CarouselItems.Add(ci);
                }
                eShopDbContext.SaveChanges();
            }
        }

        public List<CarouselItem> GenerateCarouselItems()
        {
            List<CarouselItem> carouselItems = new List<CarouselItem>()
            {
                new CarouselItem("/img/image1.jpg", "Tech 1"),
                new CarouselItem("/img/image2.jpg", "Tech 2"),
                new CarouselItem("/img/image3.jpg", "Tech 3"),
                new CarouselItem("/img/image4.jpg", "Tech 4")
            };

            return carouselItems;
        }

        public List<ProductItem> GenerateProductItems()
        {
            List<ProductItem> productItems = new List<ProductItem>()
            {
                new ProductItem(0, "Produkt 1"),
                new ProductItem(1, "Produkt 2")
            };

            return productItems;
        }
    }
}
