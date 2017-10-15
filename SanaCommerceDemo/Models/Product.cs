using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SanaCommerceDemo.Models
{
    public class Product
    {
        [BindNever]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter the product name")]
        [Display(Name = "Product Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the product price")]
        [Display(Name = "Product Price")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }
    }
}
