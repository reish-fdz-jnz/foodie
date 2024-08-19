﻿using Dapper;
using Foodie.Web.Models;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Repositories
{
    public class CheckOutRepository : ICheckOutRepository
    {
        public CheckOutRepository()
        {
        }

        public async Task CheckOut(Order order, List<OrderDetail> orderDetails)
        {
           
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                connection.Open();

                using (var tran = connection.BeginTransaction())
                {
                    string sqlOrder = "INSERT INTO [AspNetOrder] (Total,CustomerId,PaymentMethodId,Address,OrderDateTimeUTC)  VALUES (@total,@customerId, @paymentMethodId, @address, @orderDateTimeUTC); " +
                        "SELECT CAST(SCOPE_IDENTITY() as int);";
                    object parameters = new
                    {
                        Total = order.Total,
                        CustomerId = order.CustomerId,
                        PaymentMethodId = order.PaymentMethodId,
                        Address = order.Address,
                        OrderDateTimeUTC = order.OrderDateTimeUTC,
                    };
                    order.Id = await connection.ExecuteScalarAsync<int>(sqlOrder, parameters, tran);

                    tran.Commit();
                }
              
            }
        }

    }
}