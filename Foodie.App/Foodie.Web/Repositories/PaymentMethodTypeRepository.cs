using Dapper;
using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Repositories
{
    public class PaymentMethodTypeRepository : IPaymentMethodTypeRepository
    {
        public PaymentMethodTypeRepository()
        {
        }

        public async Task<List<PaymentMethodType>> GetPaymentMethodTypes()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[Name] FROM  [AspNetPaymentMethodType]";
                IEnumerable<PaymentMethodType> paymentTypes = await connection.QueryAsync<PaymentMethodType>(sql);
                return paymentTypes.ToList();
            }
        }
    }
}