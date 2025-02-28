using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Application.Data
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
