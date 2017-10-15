using System.Collections.Generic;

namespace SanaCommerceDemo.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string Title { get; set; }
    }
}
