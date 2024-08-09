using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await  productRepository.GetProducts();
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId) 
        {
            return await productRepository.GetProductsByCategoryId(categoryId);
        }
    }
}