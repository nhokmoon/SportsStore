using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels
{
    public class ProductAddViewModel
    {
        [Required(ErrorMessage = "Please enter the product name")]
        public string Name { get; set; }

        
        [Required(ErrorMessage = "Please enter the description")]
        public string Description { get; set; }

        
        [Required(ErrorMessage = "Please enter the category")]
        public string Category { get; set; }


        [Required(ErrorMessage = "Please enter the price")]
        public float Price { get; set; }

        public IFormFile Photo { get; set; }
    }
}
