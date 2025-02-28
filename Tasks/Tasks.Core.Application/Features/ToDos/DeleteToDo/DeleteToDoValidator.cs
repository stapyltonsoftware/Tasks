using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Features.ToDos.UpdateToDo;

namespace Tasks.Core.Application.Features.ToDos.DeleteToDo
{
    public class DeleteToDoValidator : AbstractValidator<DeleteToDoCommand>
    {
        public DeleteToDoValidator()
        {
            RuleFor(x => x.ToDoId).GreaterThan(0);
        }
    }
}