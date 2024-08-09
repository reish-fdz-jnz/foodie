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

        public async Task<ApplicationUser> GetUserById(string id)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "SELECT [Id],[FirstName],[LastName],[UserName],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount] FROM [AspNetUsers] " +
                    "WHERE [Id]=@id;";
                ApplicationUser user = await connection.QuerySingleAsync<ApplicationUser>(sql, new { id = id});
                return user;
            }
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser user) 
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                string sql = "UPDATE [AspNetUsers] SET [FirstName]=@firstName,[LastName]=@lastName,[Email]=@email,[PhoneNumber]=@phoneNumber " +
                    "WHERE [Id]=@id;";

                object parameters = new
                {
                    id = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber
                };
                await connection.ExecuteAsync(sql,parameters);
                
            }
            return await this.GetUserById(user.Id);
        }
    }
}