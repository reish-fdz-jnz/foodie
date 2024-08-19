using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Web.Repositories
{
    public interface IShoppingCartRepository
    {
        Task InsertItem(Cart cart);

        Task DeleteItemById(int id);

        Task UpdateItem(Cart cart);
        Task<Cart> GetItemByProductIdAndUserId(int productId, string userId);
        Task<List<Cart>> GetItemsByUserId(string userId);

        Task<List<ProductByCart>> GetItemsByUserFromCartAndProduct(string userId);
    }
}
