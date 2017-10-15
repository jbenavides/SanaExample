using System.Collections.Generic;
using System.Linq;
using SanaCommerceDemo.Data;

namespace SanaCommerceDemo.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts { get; }
        Product GetProductById(int productId);
        void CreateProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(int productId);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> GetProducts => _appDbContext.Products;

        public Product GetProductById(int productId)
        {
            return _appDbContext.Products.FirstOrDefault(x => x.ProductId == productId);
        }
        public void CreateProduct(Product product)
        {
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
        }

        public void EditProduct(Product productToEdit)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.ProductId == productToEdit.ProductId);

            if (product == null) return;

            product.Name = productToEdit.Name;
            product.Price = productToEdit.Price;

            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product == null) return;

            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();
        }
    }
}
