using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Web.Services
{
    public interface ICheckOutService
    {
        decimal DeliveryFee {get; set;}

        decimal ServiceFee { get; set; }

        Task CheckOut(CheckOut checkOut, List<Cart> carts);

        decimal CalculateTotalPrice(decimal subTotal);
    }
}
