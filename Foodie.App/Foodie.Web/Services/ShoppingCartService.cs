using Foodie.Web.Models;
using Foodie.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foodie.Web.Services
{
    public  class ShoppingCartService : IShoppingCartService
    {
        private IShoppingCartRepository shoppingCartRepository;

        private IProductService productService;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductService productService) 
        { 
            this.shoppingCartRepository = shoppingCartRepository;
            this.productService = productService;
        }

        public async Task InsertOrUpdateItem(Cart cart)
        {
            Cart foundCart = await shoppingCartRepository.GetItemByProductIdAndUserId(cart.ProductId, cart.UserId);

            if (foundCart == null)
            {
                cart.Quantity = 1;
                await shoppingCartRepository.InsertItem(cart);
            }
            else 
            {
                cart.Id = foundCart.Id;
                cart.Quantity = foundCart.Quantity + 1;
                await shoppingCartRepository.UpdateItem(cart);
            }
        }

        public async Task DeleteItemById(int id)
        {
            await shoppingCartRepository.DeleteItemById(id);
        }


        public async Task<Cart> GetItemByProductIdAndUserId(int productId, string userId) 
        {
            return await shoppingCartRepository.GetItemByProductIdAndUserId(productId,userId);
        }

        public async Task<List<Cart>> GetItemsByUserId(string userId)
        {
            List<Cart> carts = await shoppingCartRepository.GetItemsByUserId(userId);

            List<int> productIds = carts.Select(product => product.ProductId).ToList();

            List<Product> products = await this.productService.GetProductsByIds(productIds);

            foreach (Cart cart in carts)
            {
                Product product = products.FirstOrDefault(p => p.Id == cart.ProductId);
                if (product != null) 
                {
                    cart.Product =product;
                }
                
            }
            return carts;
        }


        public async Task<int> GetItemsCountByUserId(string userId)
        {
            List<Cart> carts = await shoppingCartRepository.GetItemsByUserId(userId);

            return carts.Count();
        }
    }
}