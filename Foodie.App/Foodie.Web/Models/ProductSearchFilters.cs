using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodie.Web.Models
{
    public class ProductSearchFilters
    {
        public string CategoryId { get; set; }

        public string RatingId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numeric values.")]
        public int Quantity { get; set; }

        public string Name { get; set; }
        
    }
}