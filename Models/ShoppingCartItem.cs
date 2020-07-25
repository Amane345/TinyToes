using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyToes.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public string ShoppingCartId { get; set; }
        public Clothes Clothes { get; set; }
        public int Amount { get; set; }
    }
}
