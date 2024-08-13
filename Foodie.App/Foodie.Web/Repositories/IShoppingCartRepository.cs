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

        Task UpdateItem(Cart cart);
        Task<Cart> GetItemByProductIdAndUserId(int productId, string userId);
    }
}
