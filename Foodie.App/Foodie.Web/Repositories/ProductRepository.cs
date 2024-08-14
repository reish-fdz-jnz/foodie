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
                IEnumerable<Product> products = await connection.QueryAsync<Product>(sql);
                return products.ToList();
            }
        }

        public async Task<List<Product>> GetProductsByIds(List<int> productIds)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[Name],[Price],[Quantity],[Rating],[ImageUrl],[CategoryId] FROM [AspNetProduct] " +
                "WHERE [Id] IN @ids; ";
                object parameters = new
                {
                    ids = productIds.ToArray()
                };

                IEnumerable<Product> products = await connection.QueryAsync<Product>(sql,parameters);
                return products.ToList();
            }
        }
    }
}