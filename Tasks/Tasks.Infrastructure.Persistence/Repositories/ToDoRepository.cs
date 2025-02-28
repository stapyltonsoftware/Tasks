using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Domain.Entities;

namespace Tasks.Infrastructure.Persistence.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TasksDbContext _dbContext;

        public ToDoRepository(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ToDo> AddToDoAsync(ToDo toDo)
        {
            toDo.Created = DateTime.Now;
            await _dbContext.ToDos.AddAsync(toDo);
            return toDo;
        }

        public async Task DeleteToDoAsync(ToDo toDo)
        {
            _dbContext.ToDos.Remove(toDo);
            await Task.CompletedTask;
        }

        public async Task<ToDo> GetToDoAsync(int id)
        {
            return await _dbContext.ToDos
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ToDo>> GetToDosAsync(bool includeCompleted)
        {
            var query = _dbContext.ToDos
                                .Include(x => x.Category)
                                .AsQueryable();

            if (!includeCompleted)
                query = query.Where(x => !x.Completed.HasValue);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ToDo>> SearchToDosAsync(bool includeCompleted, string search)
        {
            var query = _dbContext.ToDos.Where(x => x.Description.ToLower().Contains(search.ToLower()))
                .Include(x => x.Category)
                .AsQueryable();

            if (!includeCompleted)
                query = query.Where(x => !x.Completed.HasValue);

            return await query.ToListAsync();
        }

        public async Task<ToDo> UpdateToDoAsync(ToDo toDo)
        {
            _dbContext.ToDos.Update(toDo);
            return await Task.FromResult<ToDo>(toDo);
        }
    }
}
