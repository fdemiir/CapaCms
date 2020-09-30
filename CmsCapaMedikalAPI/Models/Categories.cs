using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCapaMedikalAPI.Models
{
    public class Categories
   {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPath { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryInfo { get; set; }
        [NotMapped]
        public List<Products> Items { get; set; }
    }
}
