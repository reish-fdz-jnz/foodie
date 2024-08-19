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

namespace Foodie.Web.Controllers
{
    public class PaymentController : Controller
    {
        IPaymentMethodService paymentMethodService;
        IPaymentMethodTypeService paymentMethodTypeService;

        public PaymentController() 
        {
            this.paymentMethodTypeService = new PaymentMethodTypeService(new PaymentMethodTypeRepository());
            this.paymentMethodService = new PaymentMethodService(new PaymentMethodRepository());
        }
        // GET: Payment
        public async Task<ActionResult> Index()
        {
            List<PaymentMethod> paymentMethods = await this.paymentMethodService.GetPaymentMethodsByUserId(User.Identity.GetUserId());
            ViewBag.PaymentMethods = paymentMethods;

            List<PaymentMethodType> paymentMethodTypes = await this.paymentMethodTypeService.GetPaymentMethodTypes();
            ViewBag.PaymentMethodTypes = this.MapPaymentMethodTypesToSelectListItems(paymentMethodTypes);

            PaymentMethodViewModel paymentMethodViewModel = new PaymentMethodViewModel()
            {
                PaymentMethod = new PaymentMethod(),
                PaymentMethodTypeId = "-1"

            };

            return View(paymentMethodViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(PaymentMethodViewModel paymentMethodViewModel)
        {

            if (!ModelState.IsValid)
            {
                List<PaymentMethod> paymentMethods = await this.paymentMethodService.GetPaymentMethodsByUserId(User.Identity.GetUserId());
                ViewBag.PaymentMethods = paymentMethods;

                List<PaymentMethodType> paymentMethodTypes = await this.paymentMethodTypeService.GetPaymentMethodTypes();
                ViewBag.PaymentMethodTypes = this.MapPaymentMethodTypesToSelectListItems(paymentMethodTypes);

                return View(paymentMethodViewModel);
            }
            paymentMethodViewModel.PaymentMethod.PaymentMethodTypeId = Int32.Parse(paymentMethodViewModel.PaymentMethodTypeId);

            await this.paymentMethodService.InsertPaymentMethod(paymentMethodViewModel.PaymentMethod);


            return RedirectToAction("Index", "Payment");
        }
        private List<SelectListItem> MapPaymentMethodTypesToSelectListItems(List<PaymentMethodType> paymentMethodTypes)
        {
            List<SelectListItem> items = paymentMethodTypes.Select(name => new SelectListItem()
            {
                Value = name.Id.ToString(),
                Text = name.Name,
            }).ToList();

            items.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "Select Method",
                Selected = true
            });

            return items;
        }


    }
}