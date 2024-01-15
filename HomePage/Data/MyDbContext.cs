using Microsoft.EntityFrameworkCore;
using HomePage.Models;
using System.Drawing;

namespace HomePage.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

      
        public DbSet<Product> Products { get; set; }
    }


}