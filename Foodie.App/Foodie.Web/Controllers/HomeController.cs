using Foodie.Web.Models;
using Foodie.Web.Repositories;
using Foodie.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace Foodie.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService productService;

        private ICategoryService categoryService;

        public HomeController() 
        {
            this.productService = new ProductService(new ProductRepository());
            this.categoryService = new CategoryService(new CategoryRepository());
        }


        public async Task<ActionResult> Index(string categoryId = "0")
        {
            List<Product> products = null;
            int id = Int32.Parse(categoryId);
            if (id == 0)
            {
                products = await this.productService.GetProducts();

            }
            else 
            {
                products = await this.productService.GetProductsByCategoryId(id);
            }
           
            List<Category> categories = await this.categoryService.GetCategories();
            categories.Add(new Category() { 
                Id = 0,
                Name = "All"
            });

            ViewBag.Categories = categories.Select(category => new SelectListItem()
            {
                Value = category.Id.ToString(),
                Text = category.Name,
                Selected = id == 0
            }).ToList();
            TempData["selectedCategoryId"] = id;
            TempData.Keep("selectedCategoryId");

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> ProductsByCategoryId(string categoryId) 
        {
            int id = Int32.Parse(categoryId);
            if (id == 0) 
            {
                List<Product> allProducts = await this.productService.GetProducts();
                return View(allProducts);
            }

            List<Product> filteredProducts = await this.productService.GetProductsByCategoryId(id);

            return RedirectToAction("Index","Home",filteredProducts);
        }
    }
}