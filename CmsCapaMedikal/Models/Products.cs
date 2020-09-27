using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsCapaMedikal.Models
{
    public class Products
    {
        public int Id { get; set; }

        [DisplayName("Ürün Kodu")]
        public string ProductCode { get; set; }
        
        [DisplayName("Ürün Adı")]
        public string ProductName { get; set; }
        
        [DisplayName("Genel Kullanım Alanı")]
        public string ProductArea { get; set; }
        
        [DisplayName("Ürün Sınıfı")]
        public string ProductClass { get; set; }
        
        [DisplayName("Ürün Tipi")]
        public string ProductType { get; set; }
        
        [DisplayName("Alt Marka")]
        public string ProductBottomBrand { get; set; }

        [DisplayName("Ürün Resim")]
        public string ProductImage { get; set; }

        [DisplayName("Marka")]
        public string ProductCategoryName { get; set; }
        [NotMapped]
        public IFormFile ProductsFile { get; set; }


    }
}
