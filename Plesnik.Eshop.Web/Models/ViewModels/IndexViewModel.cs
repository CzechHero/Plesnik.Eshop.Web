using Plesnik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.ViewModels
{
    public class IndexViewModel
    {
        public IList<CarouselItem> CarouselItems { get; set; }
        public IList<ProductItem> ProductItems { get; set; }
    }
}
