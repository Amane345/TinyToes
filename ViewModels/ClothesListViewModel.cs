using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyToes.Models;

namespace TinyToes.ViewModels
{
    public class ClothesListViewModel
    {
        public IEnumerable<Clothes> Cloth { get; set; }
        public string CurrentCategory { get; set; }

    }
}
