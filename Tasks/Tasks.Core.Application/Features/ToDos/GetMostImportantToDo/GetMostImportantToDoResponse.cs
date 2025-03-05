using Tasks.Core.Application.DTOs;

namespace Tasks.Core.Application.Features.ToDos.GetMostImportantToDo
{
    public class GetMostImportantToDoResponse(ToDoDTO toDo)
    {
        public ToDoDTO ToDo { get; } = toDo;
    }
}