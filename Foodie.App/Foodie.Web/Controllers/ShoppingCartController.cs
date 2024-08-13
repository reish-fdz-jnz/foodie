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
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartService shoppingCartService;

        public ShoppingCartController() 
        {
            this.shoppingCartService = new ShoppingCartService(new ShoppingCartRepository());
        }

        //// GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToCart(Cart cart)
        {
            await shoppingCartService.InsertOrUpdateItem(cart);

            return RedirectToAction("Index", "Home");
        }
    }
}