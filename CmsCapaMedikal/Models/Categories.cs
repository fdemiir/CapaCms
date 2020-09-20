using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCapaMedikal.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public string Info { get; set; }
        public List<Products> Items { get; set; }

    }
}
