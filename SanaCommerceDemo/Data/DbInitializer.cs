namespace SanaCommerceDemo.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new List<Product>
            {
                new Product{ Name = "Laptop", Price = 700},
                new Product{ Name = "Book", Price = 50},
                new Product{ Name = "Pen", Price = 5},
                new Product{ Name = "Short", Price = 80},
                new Product{ Name = "Skirt", Price = 100},
                new Product{ Name = "Mouse", Price = 60},
                new Product{ Name = "Keyboard", Price = 50},
                new Product{ Name = "Smartphone", Price = 400},
                new Product{ Name = "Cables", Price = 12}
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}
