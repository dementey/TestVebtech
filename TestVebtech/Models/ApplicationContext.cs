using Microsoft.EntityFrameworkCore;

namespace TestVebtech.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" },
                new Role { Id = 3, Name = "Support" },
                new Role { Id = 4, Name = "SuperAdmin" }
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Anna", Age = 22, Email = "User123@mail.ru" },
                new User { Id = 2, Name = "Dima", Age = 36, Email = "Dima322@mail.ru" },
                new User { Id = 3, Name = "Olga", Age = 15, Email = "Olga002@mail.ru" }
                );
        }
    }
}
