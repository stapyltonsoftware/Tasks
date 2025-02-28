using Microsoft.EntityFrameworkCore;
using Tasks.Core.Domain.Entities;


namespace Tasks.Infrastructure.Persistence
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) 
            :base(options) 
        {
            
        }

        public DbSet<ToDo> ToDos { get; set; } 
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasKey(x => x.Id);

            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new Category { Id = 1, Name = "Home"},
                new Category { Id = 2, Name = "Work"},
                new Category { Id = 3, Name = "Side Hustle"},
            });
        }
    }
}
