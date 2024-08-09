using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        { 
            this.userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserById(string id) 
        {
            return await this.userRepository.GetUserById(id);
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser user) 
        {
            return await userRepository.UpdateUser(user);
        }
    }
}