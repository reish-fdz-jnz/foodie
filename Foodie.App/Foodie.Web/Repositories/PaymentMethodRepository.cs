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
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        public PaymentMethodRepository() 
        { 
        }

        public async Task InsertPaymentMethod(PaymentMethod paymentMethod)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "INSERT INTO [AspNetPaymentMethod] (CardNumber,CvvNumber,ExpiryDate,Country, PaymentMethodTypeId, UserId)  " +
                    "VALUES (@cardNumber,@cvvNumber,@expiryDate, @country, @paymentMethodTypeId, @userId)";
                object parameters = new
                {
                    CardNumber = paymentMethod.CardNumber,
                    CvvNumber = paymentMethod.CvvNumber,
                    ExpiryDate = paymentMethod.ExpiryDate,
                    Country = paymentMethod.Country,
                    PaymentMethodTypeId = paymentMethod.PaymentMethodTypeId,
                    UserId = paymentMethod.UserId,
                };
                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsByUserId(string userId)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[UserId],[Country],[CardNumber],[CvvNumber],[ExpiryDate],[PaymentMethodTypeId] " +
                    "FROM [AspNetPaymentMethod] " +
                    "WHERE [UserId]=@userId";

                object parameters = new 
                {
                    UserId = userId,
                };
                IEnumerable<PaymentMethod> payments = await connection.QueryAsync<PaymentMethod>(sql, parameters);
                return payments.ToList();
            }
        }
    }
}