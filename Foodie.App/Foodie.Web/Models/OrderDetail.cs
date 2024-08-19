﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}