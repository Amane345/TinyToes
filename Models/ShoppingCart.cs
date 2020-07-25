using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyToes.Models
{
    public class ShoppingCart
    {
        private readonly AtDbContext _atDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AtDbContext atDbContext)
        {
            _atDbContext = atDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AtDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Clothes clothes, int amount)
        {
            var shoppingCartItem = _atDbContext.ShopppingCartItems.SingleOrDefault(
                s => s.Clothes.ClothesId == clothes.ClothesId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Clothes = clothes,
                    Amount = amount
                };

                _atDbContext.ShopppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _atDbContext.SaveChanges();
        }

        public int RemoveFromCart(Clothes clothes)
        {
            var shoppingCartItem = _atDbContext.ShopppingCartItems.SingleOrDefault(
                s => s.Clothes.ClothesId == clothes.ClothesId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _atDbContext.ShopppingCartItems.Remove(shoppingCartItem);
                }
            }

            _atDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _atDbContext.ShopppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Clothes)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _atDbContext.ShopppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId);

            _atDbContext.ShopppingCartItems.RemoveRange(cartItems);
            _atDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _atDbContext.ShopppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Clothes.Price * c.Amount).Sum();

            return total;
        }
    }
}
