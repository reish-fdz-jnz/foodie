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

        public async Task<List<Product>> GetProductsByFilters(ProductSearchFilters productSearchFilters)
        {
            List<Product> products = await productRepository.GetProducts();

            List<Product> filteredProducts = new List<Product>();

            foreach (Product product in products)
            {
                if ((Int32.Parse(productSearchFilters.CategoryId) == 0 || product.CategoryId == Int32.Parse(productSearchFilters.CategoryId)) &&
                    (Int32.Parse(productSearchFilters.RatingId) == 0 || product.Rating == Int32.Parse(productSearchFilters.RatingId)) &&
                     product.Quantity >= productSearchFilters.Quantity && 
                     (productSearchFilters.Name.Equals("") || product.Name.ToLower().Contains(productSearchFilters.Name.ToLower())))
                { 
                    filteredProducts.Add(product);
                }
            }

            return filteredProducts;

        }
    }
}