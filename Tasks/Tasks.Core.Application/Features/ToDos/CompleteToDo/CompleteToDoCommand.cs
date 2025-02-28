using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.Exceptions;

namespace Tasks.Core.Application.Features.ToDos.CompleteToDo
{
    public class CompleteToDoCommand : IRequest<Unit>
    {
        public int ToDoId { get; set; }
    }

    public class CompleteToDoCommandHandler : IRequestHandler<CompleteToDoCommand, Unit>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteToDoCommandHandler(IToDoRepository toDoRepository, IUnitOfWork unitOfWork)
        {
            _toDoRepository = toDoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CompleteToDoCommand request, CancellationToken cancellationToken)
        {
            var toDo = await _toDoRepository.GetToDoAsync(request.ToDoId);

            if (toDo == null)
                throw new NotFoundException($"To Do with Id {request.ToDoId} not found");

            toDo.Complete();

            await _toDoRepository.UpdateToDoAsync(toDo);
            await _unitOfWork.SaveAsync();

            return Unit.Value;

        }
    }
}
