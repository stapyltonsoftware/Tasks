using AutoMapper;
using MediatR;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Application.Features.ToDos.AddToDo
{
    public class AddToDoCommand : IRequest<AddToDoResponse>
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class AddToDoCommandHandler : IRequestHandler<AddToDoCommand, AddToDoResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IToDoRepository _toDoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddToDoCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, IToDoRepository toDoRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _toDoRepository = toDoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddToDoResponse> Handle(AddToDoCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);

            if (category == null)
                throw new Exception($"Category of Id {request.CategoryId} not found");

            var todo = await _toDoRepository.AddToDoAsync(new ToDo 
            { 
                Description = request.Description, 
                Category = category 
            });

            await _unitOfWork.SaveAsync();

            return new AddToDoResponse(_mapper.Map<ToDoDTO>(todo));
        }
    }
}
