using System.Data.Entity;
using _1_zadatak.Models;

namespace _1_zadatak
{
    public class TodoDbContext : System.Data.Entity.DbContext
    {
        public IDbSet<TodoItem> TodoList;

        public TodoDbContext(string connectionString) : base(connectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>().HasKey(p => p.Id);
            modelBuilder.Entity<TodoItem>().Property(p => p.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(p => p.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(p => p.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(p => p.UserId).IsRequired();
        }

    }
}
