using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Application.DTOs
{
    public class ToDoDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }

        public bool HasCompleted => Completed.HasValue;

        public CategoryDTO Category { get; set; }
    }
}
