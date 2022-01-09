using Plesnik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.ViewModels
{
    public class DetailViewModel
    {
        public ProductItem Product { get; set; }
        public IList<ProductItem> RelatedProducts { get; set; }

        public DetailViewModel(ProductItem product, IList<ProductItem> relatedProducts)
        {
            Product = product;
            RelatedProducts = relatedProducts;
        }

        public DetailViewModel()
        {

        }
    }
}
