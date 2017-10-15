namespace SanaCommerceDemo.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Helpers;
    using Models;
    using System;

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            
            var products = _productRepository.GetProducts;


            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            const int pageSize = 3;
            var productModel = PaginatedList<Product>.CreateAsync(products, page ?? 1, pageSize);
            return View(productModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProductById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProductById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Price")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productRepository.CreateProduct(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost(int ProductId, [Bind("ProductId, Name, Price")] Product productToEdit)
        {
            productToEdit.ProductId = ProductId;

            try
            {
                _productRepository.EditProduct(productToEdit);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(productToEdit);
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
               _productRepository.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
