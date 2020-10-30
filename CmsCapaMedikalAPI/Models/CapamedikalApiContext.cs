using CmsCapaMedikal.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsCapaMedikalAPI.Models
{
    public class CapamedikalApiContext : DbContext
    {
        public CapamedikalApiContext(DbContextOptions<CapamedikalApiContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Careers> Careers { get; set; }
        public DbSet<Contents> Contents { get; set; }
    }
}
