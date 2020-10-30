using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsCapaMedikal.Models
{
    public class Contents
    {
        public string Id { get; set; }
        
        [DisplayName("İçerik Başlık")]
        public string ContentTitle { get; set; }
        
        [DisplayName("İçerik Açıklama")]
        public string ContentDescription { get; set; }
        
        [DisplayName("İçerik Resim")]
        public string ContentImage { get; set; }
        [NotMapped]
        public IFormFile ContentFile { get; set; }
    }
}
