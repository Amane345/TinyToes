using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyToes.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Clothes> Cloth { get; }
    }
}
