using System.Data.Entity;
using _2_zadatak.Models;

namespace _2_zadatak
{
    public class TodoDbContext : System.Data.Entity.DbContext
    {
        public IDbSet<TodoItem> TodoList { get; set; }

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
