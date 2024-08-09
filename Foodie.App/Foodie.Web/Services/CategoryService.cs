using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public class CategoryService : ICategoryService
    {

        private ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await categoryRepository.GetCategories();
        }
    }
}