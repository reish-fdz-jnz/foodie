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
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        { }

        public async Task InsertItem(Order order)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "INSERT INTO [AspNetOrder] (PaymentId,SellerId,CustomerId,Address,OrderDateTimeUTC)  VALUES (@paymentId, @sellerId, @customerId,@address,@orderDateTimeUTC)";
                object parameters = new
                {
                    PaymentId = order.PaymentId,
                    SellerId = order.SellerId,
                    CustomerId = order.CustomerId,
                    Address = order.Address,
                    OrderDateTimeUTC = order.OrderDateTimeUTC
                };
                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}