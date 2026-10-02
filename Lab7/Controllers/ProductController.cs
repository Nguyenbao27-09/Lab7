using Lab7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab7.Controllers
{
    public class ProductController : Controller
    {
        private static List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Đồ điện tử" },
            new Category { Id = 2, Name = "Thời trang" }
        };

        private static List<Product> products = new List<Product>();

        public ActionResult Index()
        {
            foreach (var p in products)
            {
                p.Category = _categories.FirstOrDefault(c => c.Id == p.CategoryId);
            }
            return View(products);
        }


        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }


            product.Category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);

            return View(product);
        }


        public ActionResult Create()
        {
            ViewBag.Categories = _categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = _categories;
                    return View(product);
                }
                products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Categories = _categories;
                return View(product);
            }
        }


        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categories;
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = _categories;
                    return View(product);
                }
                var existingProduct = products.FirstOrDefault(p => p.Id == id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = product.Name;
                existingProduct.Image = product.Image;
                existingProduct.Price = product.Price;
                existingProduct.SalePrice = product.SalePrice;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Categories = _categories;
                return View(product);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var productToDelete = products.FirstOrDefault(p => p.Id == id);
                if (productToDelete != null)
                {
                    products.Remove(productToDelete);
                }
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}