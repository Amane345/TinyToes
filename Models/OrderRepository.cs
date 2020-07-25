using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyToes.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AtDbContext _atDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AtDbContext atDbContext, ShoppingCart shoppingCart)
        {
            _atDbContext = atDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            _atDbContext.Orders.Add(order);
            _atDbContext.SaveChanges();

            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    Price = shoppingCartItem.Clothes.Price,
                    ClothesId = shoppingCartItem.Clothes.ClothesId,
                    OrderId = order.OrderId
                };

                _atDbContext.OrderDetails.Add(orderDetail);
            }

            _atDbContext.SaveChanges();
        }
    }
}
