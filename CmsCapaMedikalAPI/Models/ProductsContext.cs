using CmsCapaMedikal.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsCapaMedikalAPI.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }


    }
}
