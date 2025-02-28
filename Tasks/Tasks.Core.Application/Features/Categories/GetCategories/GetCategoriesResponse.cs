using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.DTOs;

namespace Tasks.Core.Application.Features.Categories.GetCategories
{
    public class GetCategoriesResponse(IEnumerable<CategoryDTO> categories)
    {
        public IEnumerable<CategoryDTO> Categories { get; } = categories;
    }
}
