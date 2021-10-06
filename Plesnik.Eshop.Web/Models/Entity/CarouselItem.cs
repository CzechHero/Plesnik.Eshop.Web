using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Entity
{
    public class CarouselItem
    {
        public string ImageSource { get; set; }
        public string ImageAlt { get; set; }

        public CarouselItem(string imageSource, string imageAlt)
        {
            ImageSource = imageSource;
            ImageAlt = imageAlt;
        }

        public CarouselItem()
        {

        }
    }
}
