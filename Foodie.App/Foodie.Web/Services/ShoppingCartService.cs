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
        IShoppingCartRepository shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository) 
        { 
            this.shoppingCartRepository = shoppingCartRepository;
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


        public async Task<Cart> GetItemByProductIdAndUserId(int productId, string userId) 
        {
            return await shoppingCartRepository.GetItemByProductIdAndUserId(productId,userId);
        }
    }
}