using System;
using Microsoft.EntityFrameworkCore;
using TodoManager.Domain;

namespace TodoManager.Dal
{
    public class TimeManagerContext : DbContext
    {
        public TimeManagerContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<User> User { get; set; }

    }
}
