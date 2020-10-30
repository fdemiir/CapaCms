using CmsCapaMedikal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCapaMedikal.Helper
{
    public class CapaMedikalContext : DbContext
    {
        public CapaMedikalContext(DbContextOptions<CapaMedikalContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Careers> Careers { get; set; }
        public DbSet<Contents> Contents { get; set; }
    }
}
