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
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartService shoppingCartService;

        private IPaymentMethodService paymentMethodService;


        public ShoppingCartController() 
        {
            this.paymentMethodService = new PaymentMethodService(new PaymentMethodRepository());
            this.shoppingCartService = new ShoppingCartService(new ShoppingCartRepository(), new ProductService(new ProductRepository()));
        }

        //// GET: ShoppingCart
        public async Task<ActionResult> Index()
        {
            List<Cart> carts = await this.shoppingCartService.GetItemsByUserId(User.Identity.GetUserId());

            ViewBag.Carts = carts;

            ViewBag.CartCount = await this.shoppingCartService.GetItemsCountByUserId(User.Identity.GetUserId());

            ViewBag.SubTotal = await this.shoppingCartService.CalculateSubTotalPrice(User.Identity.GetUserId());

            List<PaymentMethod> paymentMethods = await this.paymentMethodService.GetPaymentMethodsByUserId(User.Identity.GetUserId());
            ViewBag.GoToCheckOutEnabled = paymentMethods.Count > 0;

            return View();
        }

        [HttpPost] // ShoppingCart/AddToCart
        [ValidateAntiForgeryToken] 
        public async Task<ActionResult> AddToCart(Cart cart)
        {
            await shoppingCartService.InsertOrUpdateItem(cart);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost] // ShoppingCart/DeleteCart
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCart(Cart cart)
        {
            await shoppingCartService.DeleteItemById(cart.Id);

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}