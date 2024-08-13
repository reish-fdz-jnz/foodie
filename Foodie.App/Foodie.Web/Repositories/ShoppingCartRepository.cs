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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCartRepository() 
        {
        }

        public async Task InsertItem(Cart cart) 
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "INSERT INTO [AspNetCart] (UserId,ProductId,Quantity)  VALUES (@userId, @productId, @quantity)";
                object  parameters = new { 
                    UserId = cart.UserId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                };
                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task UpdateItem(Cart cart)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "UPDATE [AspNetCart] SET [Quantity]=@quantity " +
                    "WHERE [Id]=@id;";
                object parameters = new
                {
                    Id = cart.Id,
                    Quantity = cart.Quantity,
                };
                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
            }
        }


        public async Task<Cart> GetItemByProductIdAndUserId(int productId, string userId) 
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[ProductId],[UserId],[Quantity]  FROM  [AspNetCart]" +
                   "WHERE [ProductId]=@productId AND [UserId]=@userId;";
                Cart cart = await connection.QuerySingleOrDefaultAsync<Cart>(sql, new { productId = productId, userId = userId});
                return cart;
            }
        }

        public async Task<List<Cart>> GetItemsByUserId(string userId)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[ProductId],[UserId],[Quantity]  FROM  [AspNetCart]" +
                   "WHERE [UserId]=@userId;";
                IEnumerable<Cart> carts = await connection.QueryAsync<Cart>(sql, new { userId = userId });
                return carts.ToList();
            }
        }


    }
}