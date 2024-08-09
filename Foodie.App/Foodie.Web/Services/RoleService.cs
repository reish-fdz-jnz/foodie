using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<List<Role>> GetRoles()
        {
            return await roleRepository.GetRoles();
        }
    }
}