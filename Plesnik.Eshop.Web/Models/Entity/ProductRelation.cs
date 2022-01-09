using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Entity
{
    [Table(nameof(ProductRelation))]
    public class ProductRelation
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(RelatedProduct))]
        public int RelatedProductId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTimeCreated { get; protected set; }

        public ProductItem Product { get; set; }

        public ProductItem RelatedProduct { get; set; }

        public ProductRelation(int productId, int relatedProductId, ProductItem product, ProductItem relatedProduct)
        {
            ProductId = productId;
            RelatedProductId = relatedProductId;
            Product = product ?? throw new ArgumentNullException(nameof(product));
            RelatedProduct = relatedProduct ?? throw new ArgumentNullException(nameof(relatedProduct));
        }

        public ProductRelation()
        {

        }
    }
}
