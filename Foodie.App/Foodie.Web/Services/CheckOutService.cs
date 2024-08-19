using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public class CheckOutService : ICheckOutService
    {
        private ICheckOutRepository checkOutRepository;

        public decimal DeliveryFee { get; set; }
        public decimal ServiceFee { get; set; }
        public CheckOutService(ICheckOutRepository checkOutRepository) 
        {
            this.checkOutRepository = checkOutRepository;
            this.DeliveryFee = 675;
            this.ServiceFee = 450;
        }

        public async Task<decimal> CalculateTotalPrice(decimal subTotal)
        {
            decimal totalPrice = subTotal + DeliveryFee + ServiceFee;

            return totalPrice;
        }

        public async Task CheckOut(CheckOut checkOut)
        {
            await this.checkOutRepository.CheckOut(checkOut.Order, checkOut.OrderDetails);
        }


    }
}