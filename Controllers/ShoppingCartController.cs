using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyToes.Models;
using TinyToes.ViewModels;

namespace TinyToes.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IClothesRepository _clothesRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IClothesRepository clothesRepository, ShoppingCart shoppingCart)
        {
            _clothesRepository = clothesRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int clothesId)
        {
            var selectedClothes = _clothesRepository.GetAllClothes.FirstOrDefault(c => c.ClothesId == clothesId);

            if (selectedClothes != null)
            {
                _shoppingCart.AddToCart(selectedClothes, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int clothesId)
        {
            var selectedClothes = _clothesRepository.GetAllClothes.FirstOrDefault(c => c.ClothesId == clothesId);

            if (selectedClothes != null)
            {
                _shoppingCart.RemoveFromCart(selectedClothes);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }
    }
}