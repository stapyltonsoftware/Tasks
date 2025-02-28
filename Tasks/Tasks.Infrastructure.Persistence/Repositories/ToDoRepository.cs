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

        public async Task<IEnumerable<ToDo>> GetToDosAsync()
        {
            return await _dbContext.ToDos.ToListAsync();
        }

        public async Task<ToDo> UpdateToDoAsync(ToDo toDo)
        {
            _dbContext.ToDos.Update(toDo);
            return await Task.FromResult<ToDo>(toDo);
        }
    }
}
