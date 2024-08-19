using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public class PaymentMethodTypeService : IPaymentMethodTypeService
    {
        IPaymentMethodTypeRepository paymentTypeRepository;
        public PaymentMethodTypeService(IPaymentMethodTypeRepository paymentTypeRepository) 
        {
            this.paymentTypeRepository = paymentTypeRepository;
        }

        public async Task<List<PaymentMethodType>> GetPaymentMethodTypes()
        {
            return await this.paymentTypeRepository.GetPaymentMethodTypes();
        }
    }
}