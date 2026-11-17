using LifeManagementApp.Data;
using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Services
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext db;

        public TodoRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await db.Todos.ToListAsync();
        }

        public async Task<List<TodoItem>> GetTop3Async()
        {
            return await db.Todos
                .Where(t => !t.IsDone)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .Take(3)
                .ToListAsync();
        }

        public async Task AddAsync(TodoItem item)
        {
            db.Todos.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            db.Todos.Update(item);
            await db.SaveChangesAsync();
        }
    }
}