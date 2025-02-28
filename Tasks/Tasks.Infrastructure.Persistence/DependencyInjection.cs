using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Infrastructure.Persistence.Repositories;

namespace Tasks.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TasksDbContext>(options =>
                     options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Tasks.Infrastructure.Persistence")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
