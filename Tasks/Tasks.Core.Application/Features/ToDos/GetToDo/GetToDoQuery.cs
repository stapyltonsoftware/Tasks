using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Application.Exceptions;

namespace Tasks.Core.Application.Features.ToDos.GetToDo
{
    public class GetToDoQuery : IRequest<GetToDoResponse>
    {
        public int ToDoId { get; set; }
    }

    public class GetToDoQueryHandler : IRequestHandler<GetToDoQuery, GetToDoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IToDoRepository _toDoRepository;

        public GetToDoQueryHandler(IMapper mapper, IToDoRepository toDoRepository)
        {
            _mapper = mapper;
            _toDoRepository = toDoRepository;
        }
        public async Task<GetToDoResponse> Handle(GetToDoQuery request, CancellationToken cancellationToken)
        {
            var toDo = await _toDoRepository.GetToDoAsync(request.ToDoId);

            if (toDo == null)
                throw new NotFoundException($"ToDo of Id {request.ToDoId} not found");

            return new GetToDoResponse(_mapper.Map<ToDoDTO>(toDo));
        }
    }
}
