using Microsoft.AspNetCore.Http;
using Plesnik.Eshop.Web.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Models.Entity
{
    [Table(nameof(CarouselItem))]
    public class CarouselItem
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ContentType("image")]
        [NotMapped]
        public IFormFile Image { get; set; }

        [StringLength(255)]
        [Required]
        public string ImageSource { get; set; }
        [StringLength(50)]
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
