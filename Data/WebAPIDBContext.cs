using A1.Models;
using Microsoft.EntityFrameworkCore;
namespace A1.Data
{
    public class WebAPIDBContext : DbContext
    {
        public WebAPIDBContext(DbContextOptions<WebAPIDBContext> options) : base(options) { }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}

