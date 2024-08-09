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
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository()
        {
        }
        public async Task<List<Category>> GetCategories()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[Name] FROM [AspNetCategory]";
                IEnumerable<Category> users = await connection.QueryAsync<Category>(sql);
                return users.ToList();
            }
        }
    }
}