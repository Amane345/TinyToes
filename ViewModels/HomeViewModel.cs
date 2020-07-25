using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyToes.Models;

namespace TinyToes.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Clothes> ClothesOnSale { get; set; }
        public IEnumerable<Clothes> ClothesInStock { get; set; }
    }
}
