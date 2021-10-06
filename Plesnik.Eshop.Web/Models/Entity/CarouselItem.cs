using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Entity
{
    public class CarouselItem
    {
        public int Id { get; set; }
        public string ImageSource { get; set; }
        public string ImageAlt { get; set; }

        public CarouselItem(int id, string imageSource, string imageAlt)
        {
            Id = id;
            ImageSource = imageSource;
            ImageAlt = imageAlt;
        }

        public CarouselItem()
        {

        }
    }
}
