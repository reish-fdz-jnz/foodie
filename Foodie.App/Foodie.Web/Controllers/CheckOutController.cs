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
using System.Xml.Linq;

namespace Foodie.Web.Controllers
{
    public class CheckOutController : Controller
    {
        private ICheckOutService checkOutService;

        private IPaymentMethodService paymentMethodService;

        private IShoppingCartService shoppingCartService;


        public CheckOutController()
        {
            this.checkOutService = new CheckOutService(new CheckOutRepository());
            this.paymentMethodService = new PaymentMethodService(new PaymentMethodRepository());
            this.shoppingCartService = new ShoppingCartService(new ShoppingCartRepository(), new ProductService(new ProductRepository()));
        }


        // GET: CheckOut
        public async Task<ActionResult> Index()
        {
            CheckOut checkOut = new CheckOut();
            decimal subTotal = await this.shoppingCartService.CalculateSubTotalPrice(User.Identity.GetUserId());
            decimal total = await this.checkOutService.CalculateTotalPrice(subTotal);
            checkOut.PaymentMethodId = "-1";
            checkOut.SubTotal = subTotal;
            checkOut.DeliveryFee = this.checkOutService.DeliveryFee;
            checkOut.ServiceFee = this.checkOutService.ServiceFee;
            checkOut.Order = new Order()
            {
                Total = total,
                CustomerId = User.Identity.GetUserId(),
                OrderDateTimeUTC = DateTime.UtcNow,
            };

            List<PaymentMethod> paymentMethods = await this.paymentMethodService.GetPaymentMethodsByUserId(User.Identity.GetUserId());

            ViewBag.PaymentMethods = this.MapPaymentMethodsToSelectListItems(paymentMethods);

            return View(checkOut);
        }

        //POST : CheckOut/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckOut checkOut)
        {
            if (!ModelState.IsValid)
            {
                List<PaymentMethod> paymentMethods = await this.paymentMethodService.GetPaymentMethodsByUserId(User.Identity.GetUserId());

                ViewBag.PaymentMethods = this.MapPaymentMethodsToSelectListItems(paymentMethods);

                return View(checkOut);
            }
            checkOut.Order.PaymentMethodId = Int32.Parse(checkOut.PaymentMethodId);

            await this.checkOutService.CheckOut(checkOut);

            return RedirectToAction("Index", "Home");
        }

        private List<SelectListItem> MapPaymentMethodsToSelectListItems(List<PaymentMethod> paymentMethods)
        {
            List<SelectListItem> items = paymentMethods.Select(paymentMethod => new SelectListItem()
            {
                Value = paymentMethod.Id.ToString(),
                Text = paymentMethod.CardNumber,
            }).ToList();

            items.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "Select Payment Method",
                Selected = true
            });

            return items;
        }

    }
}