using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodie.Web.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int CvvNumber { get; set; }

        public int PaymentMethodTypeId { get; set; }

    }
}