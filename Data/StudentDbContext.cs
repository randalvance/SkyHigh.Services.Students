using Microsoft.EntityFrameworkCore;
using SkyHigh.Services.Students.Models;

namespace SkyHigh.Services.Students.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Student> Students { get; set; }
    }
}