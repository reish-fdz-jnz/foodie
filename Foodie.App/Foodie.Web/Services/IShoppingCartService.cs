using Foodie.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Web.Services
{
    public interface IShoppingCartService
    {
        Task InsertOrUpdateItem(Cart cart);

        Task DeleteItemById(int id);

        Task<Cart> GetItemByProductIdAndUserId(int productId, string userId);

        Task<List<Cart>> GetItemsByUserId(string userId);

        Task<int> GetItemsCountByUserId(string userId);
    }
}
