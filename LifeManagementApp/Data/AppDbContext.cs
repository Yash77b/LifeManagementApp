using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;


namespace LifeManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> Todos => Set<TodoItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}