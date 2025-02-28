using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Core.Application.Features.ToDos.UpdateToDo
{
    public class UpdateToDoValidator : AbstractValidator<UpdateToDoCommand>
    {
        public UpdateToDoValidator()
        {
            RuleFor(x => x.ToDoId).GreaterThan(0); 
            RuleFor(x => x.CategoryId).GreaterThan(0); 
            RuleFor(x => x.Description).NotEmpty().NotNull(); 
        }
    }
}
