using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodie.Web.Services
{
    public class UserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        { 
            this.userRepository = userRepository;
        }
    }
}