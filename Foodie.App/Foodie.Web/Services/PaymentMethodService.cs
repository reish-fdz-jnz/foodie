using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        IPaymentMethodRepository paymentRepository;
        public PaymentMethodService(IPaymentMethodRepository paymentRepository) 
        {
            this.paymentRepository = paymentRepository;
        }
        public async Task InsertPaymentMethod(PaymentMethod paymentMethod)
        {
            await this.paymentRepository.InsertPaymentMethod(paymentMethod);
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsByUserId(string userId) 
        {
            return await this.paymentRepository.GetPaymentMethodsByUserId(userId);
        }
    }
}