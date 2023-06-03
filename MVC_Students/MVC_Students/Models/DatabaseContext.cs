using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MVC_Students.Models
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        
        public DatabaseContext(DbContextOptions options) : base(options) { }
        protected DatabaseContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>().HasData(
                new Student(1, "Jakub", "Bilinski", "s22705"),
                new Student(2, "Kuba", "Bili", "s24234"),
                new Student(3, "Jan", "Biniek", "s55555")
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
