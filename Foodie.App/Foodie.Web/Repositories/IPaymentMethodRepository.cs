using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Web.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task InsertPaymentMethod(PaymentMethod paymentMethod);
        Task<List<PaymentMethod>> GetPaymentMethodsByUserId(string userId);
    }
}
