using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Application.Data
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDo>> GetToDosAsync();
        Task<ToDo> AddToDoAsync(ToDo toDo);
        Task<ToDo> UpdateToDoAsync(ToDo toDo);
        Task DeleteToDoAsync(ToDo toDo);
    }
}
