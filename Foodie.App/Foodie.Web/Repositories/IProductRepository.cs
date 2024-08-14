using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Web.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();

        Task<List<Product>> GetProductsByIds(List<int> productIds);
    }
}
