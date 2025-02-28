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
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Application.Features.ToDos.UpdateToDo
{
    public class UpdateToDoCommand : IRequest<Unit>
    {
        public int ToDoId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateToDoCommandHandler : IRequestHandler<UpdateToDoCommand, Unit>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateToDoCommandHandler(IToDoRepository toDoRepository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _toDoRepository = toDoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<Unit> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
        {
            var toDo = await _toDoRepository.GetToDoAsync(request.ToDoId);

            if (toDo == null)
                throw new NotFoundException($"ToDo of Id {request.ToDoId} not found");

            toDo.Description = request.Description;

            if (toDo.Category.Id != request.CategoryId)
            {
                await AssignCorrectCategory(toDo, request.CategoryId);
            }

            await _toDoRepository.UpdateToDoAsync(toDo);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }

        private async Task AssignCorrectCategory(ToDo toDo, int categoryId)
        {
            var newCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            if (newCategory == null)
                throw new Exception($"Category with Id {categoryId} not found");

            toDo.Category = newCategory;
        }
    }
}
