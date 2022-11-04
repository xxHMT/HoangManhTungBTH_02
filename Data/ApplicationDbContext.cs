using HoangManhTungBTH_02.Models;
using Microsoft.EntityFrameworkCore;

namespace HoangManhTungBTH_02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students {get; set;}

        public DbSet<HoangManhTungBTH_02.Models.Employee>Employee {get; set;}
    }
}
