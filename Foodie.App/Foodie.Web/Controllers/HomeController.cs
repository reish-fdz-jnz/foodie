using Foodie.Web.Models;
using Foodie.Web.Repositories;
using Foodie.Web.Services;
using Microsoft.AspNet.Identity;
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

        private IShoppingCartService shoppingCartService;

        public HomeController() 
        {
            this.productService = new ProductService(new ProductRepository());
            this.categoryService = new CategoryService(new CategoryRepository());
            this.shoppingCartService = new ShoppingCartService(new ShoppingCartRepository(), new ProductService(new ProductRepository()));
        }


        public async Task<ActionResult> Index()
        {
            List<Product> products = await this.productService.GetProducts(); 
            List<Category> categories = await this.categoryService.GetCategories();

            ViewBag.Categories = this.MapCategoriesToSelectListItems(categories);
            
            ViewBag.Ratings = this.MapRatingsToSelectListItems();

            ViewBag.Products = products;

            ViewBag.CartCount = await this.shoppingCartService.GetItemsCountByUserId(User.Identity.GetUserId());

            ProductSearchFilters productSearchFilters = new ProductSearchFilters()
            {
               CategoryId = "0",
               RatingId = "0",
               Quantity = 0,
               Name = ""
            };

            return View(productSearchFilters);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ProductSearchFilters productSearchFilters) 
        {
            List<Product> products = await this.productService.GetProductsByFilters(productSearchFilters);

            List<Category> categories = await this.categoryService.GetCategories();

            ViewBag.Categories = this.MapCategoriesToSelectListItems(categories);

            ViewBag.Ratings = this.MapRatingsToSelectListItems();

            ViewBag.Products = products;

            return View(productSearchFilters);
        }

        private List<SelectListItem> MapCategoriesToSelectListItems(List<Category> categories) 
        {
            List<SelectListItem> items = categories.Select(category => new SelectListItem()
            {
                Value = category.Id.ToString(),
                Text = category.Name
            }).ToList();

            items.Add(new SelectListItem()
            {
                Value = "0",
                Text = "All Categories",
                Selected = true
            });

            return items;
        }

        private List<SelectListItem> MapRatingsToSelectListItems( ) 
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "1",
                    Text = "1 Star"
                },
                 new SelectListItem()
                {
                    Value = "2",
                    Text = "2 Stars"
                },
                  new SelectListItem()
                {
                    Value = "3",
                    Text = "3 Stars"
                },
                 new SelectListItem()
                {
                    Value = "4",
                    Text = "4 Stars"
                },
                  new SelectListItem()
                {
                    Value = "5",
                    Text = "5 Stars"
                },
                new SelectListItem()
                {
                    Value = "0",
                    Text = "All Ratings",
                    Selected = true
                }
            };


        }
    }
}