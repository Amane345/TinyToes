using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TinyToes.Models
{
    public class ClothesRepository : IClothesRepository
    {
        private readonly AtDbContext _appDbContext;

        public ClothesRepository(AtDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Clothes> GetAllClothes
        {
            get
            {
                return _appDbContext.Cloth.Include(c => c.Category);
            }
        }

        public IEnumerable<Clothes> GetClothesOnSale
        {
            get
            {
                return _appDbContext.Cloth.Include(c => c.Category).Where(p => p.IsOnSale);
            }
        }
        public IEnumerable<Clothes> GetClothesInStock
        {
            get
            {
                return _appDbContext.Cloth.Include(c => c.Category).Where(p => p.IsInStock);
            }
        }
        public Clothes GetClothesById(int clothesId)
        {
            return _appDbContext.Cloth.FirstOrDefault(c => c.ClothesId == clothesId);
        }
    }
}

