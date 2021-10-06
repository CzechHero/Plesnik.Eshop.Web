using Plesnik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Database
{
    public class DatabaseInit
    {
        public List<CarouselItem> GenerateCarouselItems()
        {
            List<CarouselItem> carouselItems = new List<CarouselItem>()
            {
                new CarouselItem(0, "/img/image1.jpg", "Orangutan"),
                new CarouselItem(1, "/img/image2.jpg", "Gorilla"),
                new CarouselItem(2, "/img/image3.jpg", "\"Human\""),
                new CarouselItem(3, "/img/image4.jpg", "Chimpanzee")
            };

            return carouselItems;
        }
    }
}
