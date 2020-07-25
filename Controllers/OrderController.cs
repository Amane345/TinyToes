using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyToes.Models;

namespace TinyToes.Controllers
{
    public class OrderController : Controller
    {
       
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;
        public OrderController(ShoppingCart shoppingCart, IOrderRepository orderRepository)
        {
            
            _shoppingCart = shoppingCart;
            _orderRepository = orderRepository;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for your order. Enjoy your Order";
            return View();
        }
    }
}