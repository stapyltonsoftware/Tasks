using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Core.Application.Features.ToDos.AddToDo
{
    public class AddToDoValidator :  AbstractValidator<AddToDoCommand>
    {
        public AddToDoValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
}
