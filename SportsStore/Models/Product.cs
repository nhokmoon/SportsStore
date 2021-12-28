using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }


        [Required(ErrorMessage = "Please enter the product name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter the description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Please enter the category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter the price")]
        public float Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
