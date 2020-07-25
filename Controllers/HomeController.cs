using Microsoft.AspNetCore.Mvc;
using TinyToes.Models;
using TinyToes.ViewModels;

namespace TinyToes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClothesRepository _clothesRepository;

        public HomeController(IClothesRepository clothesRepository)
        {
            _clothesRepository = clothesRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                ClothesOnSale = _clothesRepository.GetClothesOnSale,
                ClothesInStock = _clothesRepository.GetClothesInStock
            };

            return View(homeViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
