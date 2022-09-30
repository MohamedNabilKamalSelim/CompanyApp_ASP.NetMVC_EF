using CompanyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Data
{
    public class Company_DbContext : DbContext
    {
        public Company_DbContext(DbContextOptions<Company_DbContext> options)
        : base(options) {

        }
        //To add the Tables and its Schema here (IF you want)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //To add Employee Table and its Schema
            //modelBuilder.Entity<Employee>().ToTable("Employees", "HR");
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
