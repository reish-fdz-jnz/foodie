using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class Order
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public string SellerId { get; set; }

        public string CustomerId { get; set; }

        public int PaymentMethodId { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime OrderDateTimeUTC { get; set; }

    }
}