using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;

namespace Tasks.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TasksDbContext _tasksDbContext;

        public UnitOfWork(TasksDbContext tasksDbContext)
        {
            _tasksDbContext = tasksDbContext;
        }

        public async Task SaveAsync()
        {
            await _tasksDbContext.SaveChangesAsync();
        }
    }
}
