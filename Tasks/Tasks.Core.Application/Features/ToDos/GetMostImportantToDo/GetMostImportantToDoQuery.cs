using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Application.ThirdPartyServices;

namespace Tasks.Core.Application.Features.ToDos.GetMostImportantToDo
{
    public class GetMostImportantToDoQuery : IRequest<GetMostImportantToDoResponse>
    {
    }

    public class GetMostImportantToDoQueryHandler : IRequestHandler<GetMostImportantToDoQuery, GetMostImportantToDoResponse>
    {
        private readonly IToDoAIService _toDoAIService;
        private readonly IMapper _mapper;

        public GetMostImportantToDoQueryHandler(IToDoAIService toDoAIService, IMapper mapper)
        {
            _toDoAIService = toDoAIService;
            _mapper = mapper;
        }
        public async Task<GetMostImportantToDoResponse> Handle(GetMostImportantToDoQuery request, CancellationToken cancellationToken)
        {
            var toDo = await _toDoAIService.GetMostImportantToDo();

            return new GetMostImportantToDoResponse(_mapper.Map<ToDoDTO>(toDo));
        }
    }
}
