using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TinyToes.Models;
using TinyToes.ViewModels;

namespace TinyToes.Controllers
{
    public class ClothesController : Controller
    {
        private readonly IClothesRepository _clothesRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ClothesController(IClothesRepository clothesRepository, ICategoryRepository categoryRepository)
        {
            _clothesRepository = clothesRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category)
        {
            IEnumerable<Clothes> cloth;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                cloth = _clothesRepository.GetAllClothes.OrderBy(c => c.ClothesId);
                currentCategory = "All Clothes";
            }
            else
            {
                cloth = _clothesRepository.GetAllClothes.Where(c => c.Category.CategoryName == category);

                currentCategory = _categoryRepository.GetAllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new ClothesListViewModel
            {
                Cloth = cloth,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            var clothes = _clothesRepository.GetClothesById(id);
            if (clothes == null)
                return NotFound();

            return View(clothes);
        }
    }
}
