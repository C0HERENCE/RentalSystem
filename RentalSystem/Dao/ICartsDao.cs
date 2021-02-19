using System.Collections.Generic;
using RentalSystem.Models;

namespace RentalSystem.Dao
{
    public interface ICartsDao
    {
        int AddGoodsToUserCart(CartModel cartModel);
        List<CartModel> GetCartModelsByUserId(int userId, int goodsId);
    }
}