using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using System.Configuration;

namespace Foodie.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        { 
        
        }

        public async Task<List<ApplicationUser>> GetUser()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT * FROM AspNetUsers";
                IEnumerable<ApplicationUser> users = await connection.QueryAsync<ApplicationUser>(sql);
                return users.ToList();
            }
        }
    }
}