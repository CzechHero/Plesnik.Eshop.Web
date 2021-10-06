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
                new CarouselItem("/img/image1.jpg", "Orangutan"),
                new CarouselItem("/img/image2.jpg", "Gorilla"),
                new CarouselItem("/img/image3.jpg", "\"Human\""),
                new CarouselItem("/img/image4.jpg", "Chimpanzee")
            };

            return carouselItems;
        }
    }
}
