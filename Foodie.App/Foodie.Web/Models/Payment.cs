using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class Payment
    {
        public string UserId { get; set; }

        public string CardName { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Country { get; set; }

        public int CvvNumber { get; set; }

        public int PaymentTypeId { get; set; }

    }
}