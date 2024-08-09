using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int Rating { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}