using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyToes.Models
{
    public interface IClothesRepository
    {
        IEnumerable<Clothes> GetAllClothes { get; }
        IEnumerable<Clothes> GetClothesOnSale { get; }
        IEnumerable<Clothes> GetClothesInStock { get; }
        Clothes GetClothesById(int clothesId);
    }
}
