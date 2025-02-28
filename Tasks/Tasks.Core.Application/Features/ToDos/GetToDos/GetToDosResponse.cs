using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.DTOs;

namespace Tasks.Core.Application.Features.ToDos.GetToDos
{
    public class GetToDosResponse(IEnumerable<ToDoDTO> toDos)
    {
        public IEnumerable<ToDoDTO> ToDos { get; } = toDos;
    }
}
