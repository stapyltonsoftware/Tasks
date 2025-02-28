using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.Exceptions;

namespace Tasks.Core.Application.Features.ToDos.DeleteToDo
{
    public class DeleteToDoCommand : IRequest<Unit>
    {
        public int ToDoId { get; set; }
    }

    public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoRepository _toDoRepository;

        public DeleteToDoCommandHandler(IUnitOfWork unitOfWork, IToDoRepository toDoRepository)
        {
            _unitOfWork = unitOfWork;
            _toDoRepository = toDoRepository;
        }

        public async Task<Unit> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
        {
            var toDo = await _toDoRepository.GetToDoAsync(request.ToDoId);

            if (toDo == null)
                throw new NotFoundException($"ToDo with Id {request.ToDoId} not found");

            await _toDoRepository.DeleteToDoAsync(toDo);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
