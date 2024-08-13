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

        Task<Cart> GetItemByProductIdAndUserId(int productId, string userId);
    }
}
