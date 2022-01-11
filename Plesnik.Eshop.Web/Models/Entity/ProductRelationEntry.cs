using Microsoft.EntityFrameworkCore;
using Plesnik.Eshop.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Entity
{
    [Table(nameof(ProductRelationEntry))]
    public class ProductRelationEntry
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

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTimeSuggested { get; protected set; }

        public ProductItem Product { get; set; }
        public ProductItem RelatedProduct { get; set; }
        public User User { get; set; }

        public ProductRelationEntry(int productId, int relatedProductId, int userId, ProductItem product, ProductItem relatedProduct, User user)
        {
            ProductId = productId;
            RelatedProductId = relatedProductId;
            UserId = userId;
            Product = product ?? throw new ArgumentNullException(nameof(product));
            RelatedProduct = relatedProduct ?? throw new ArgumentNullException(nameof(relatedProduct));
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        public ProductRelationEntry()
        {

        }
    }
}
