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
    }
}
