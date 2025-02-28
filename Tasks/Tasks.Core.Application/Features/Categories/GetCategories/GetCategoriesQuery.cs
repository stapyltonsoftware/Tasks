using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.DTOs;

namespace Tasks.Core.Application.Features.Categories.GetCategories
{
    public class GetCategoriesQuery : IRequest<GetCategoriesResponse>
    {
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<GetCategoriesResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return new GetCategoriesResponse(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }
    }
}
