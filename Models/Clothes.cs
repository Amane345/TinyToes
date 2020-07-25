using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TinyToes.Models
{
    public class Clothes
    {
        public int ClothesId { get; set; }
        
       
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile DataFiles { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsInStock { get; set; }
       
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
