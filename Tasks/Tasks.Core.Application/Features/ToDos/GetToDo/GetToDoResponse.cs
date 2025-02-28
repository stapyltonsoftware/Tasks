using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.DTOs;

namespace Tasks.Core.Application.Features.ToDos.GetToDo
{
    public class GetToDoResponse(ToDoDTO toDo)
    {
        public ToDoDTO ToDo { get; } = toDo;
    }
}
