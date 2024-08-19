using Foodie.Web.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class CheckOut
    {
        public Order Order { get; set; }

        public decimal SubTotal { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal ServiceFee { get; set; }

        [Required]
        public string PaymentMethodId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }


    }
}