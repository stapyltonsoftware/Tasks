using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Core.Application.Features.ToDos.CompleteToDo
{
    public class CompleteToDoValidator : AbstractValidator<CompleteToDoCommand>
    {
        public CompleteToDoValidator()
        {
            RuleFor(x => x.ToDoId).GreaterThan(0);
        }
    }
}
