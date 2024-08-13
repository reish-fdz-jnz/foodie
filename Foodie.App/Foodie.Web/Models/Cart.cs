using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}