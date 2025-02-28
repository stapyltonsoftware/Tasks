using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Core.Domain.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }

        public bool HasCompleted => Completed.HasValue;

        public Category Category { get; set; }

        public void Complete()
        {
            if (HasCompleted)
            {
                throw new InvalidOperationException("Task has already completed");
            }

            Completed = DateTime.Now;
        }

        public void Uncomplete()
        {
            if (!HasCompleted)
            {
                throw new InvalidOperationException("Task hasn't completed yet");
            }

            Completed = null;
        }
    }
}
