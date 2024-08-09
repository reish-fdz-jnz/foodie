using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Repositories
{
    public class ProductRepository : IProductRepository
    {

        public ProductRepository() 
        { 
        }
        public async Task<List<Product>> GetProducts()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[Name],[Price],[Quantity],[Rating],[ImageUrl],[CategoryId] FROM [AspNetProduct]";
                IEnumerable<Product> users = await connection.QueryAsync<Product>(sql);
                return users.ToList();
            }
        }

        
        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[Name],[Price],[Quantity],[Rating],[ImageUrl],[CategoryId] FROM [AspNetProduct] " +
                    "WHERE [CategoryId]=@categoryId;";
                IEnumerable<Product> products = await connection.QueryAsync<Product>(sql, new {categoryId = categoryId });

                return products.ToList();
            }
        }
    }
}