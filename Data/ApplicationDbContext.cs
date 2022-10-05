using HoangManhTungBTH_02.Models;
using Microsoft.EntityFrameworkCore;

namespace HoangManhTungBTH_02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOption<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students {get; set;}
    }
}
