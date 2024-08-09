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
    public class RoleRepository : IRoleRepository
    {
        public RoleRepository() { 
        }

        public async Task<List<Role>> GetRoles()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[Name] FROM [AspNetRoles]";
                IEnumerable<Role> roles = await connection.QueryAsync<Role>(sql);
                return roles.ToList();
            }
        }
    }
}