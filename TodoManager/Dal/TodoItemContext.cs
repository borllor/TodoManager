using System;
using Microsoft.EntityFrameworkCore;
using TodoManager.Domain;

namespace TodoManager.Dal
{
    public class TodoItemContext : DbContext
    {
        public TodoItemContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<User> User { get; set; }

    }
}
