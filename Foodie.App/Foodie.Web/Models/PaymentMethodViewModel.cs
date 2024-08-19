using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class PaymentMethodViewModel
    {
        [RegularExpression(@"^[1-9]$", ErrorMessage = "The payment method type must be selected.")]
        public string PaymentMethodTypeId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


    }
}