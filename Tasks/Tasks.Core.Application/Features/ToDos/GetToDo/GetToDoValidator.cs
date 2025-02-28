using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Core.Application.Features.ToDos.GetToDo
{
    public class GetToDoValidator : AbstractValidator<GetToDoQuery>
    {
        public GetToDoValidator()
        {
            RuleFor(x => x.ToDoId).GreaterThan(0);
        }
    }
}
