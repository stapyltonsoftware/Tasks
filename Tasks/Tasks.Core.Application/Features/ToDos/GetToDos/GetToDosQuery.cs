using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Application.Features.ToDos.GetToDos
{
    public class GetToDosQuery : IRequest<GetToDosResponse>
    {
        public string Search { get; set; }
        public bool IncludeCompleted { get; set; }
    }

    public class GetToDosQueryHandler : IRequestHandler<GetToDosQuery, GetToDosResponse>
    {
        private readonly IMapper _mapper;
        private readonly IToDoRepository _toDoRepository;

        public GetToDosQueryHandler(IMapper mapper, IToDoRepository toDoRepository)
        {
            _mapper = mapper;
            _toDoRepository = toDoRepository;
        }

        public async Task<GetToDosResponse> Handle(GetToDosQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ToDo> todos = new List<ToDo>();

            todos = string.IsNullOrEmpty(request.Search)
                ? await _toDoRepository.GetToDosAsync(request.IncludeCompleted)
                : await _toDoRepository.SearchToDosAsync(request.IncludeCompleted, request.Search);

            return new GetToDosResponse(_mapper.Map<IEnumerable<ToDoDTO>>(todos));
        }
    }
}
