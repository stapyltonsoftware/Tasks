using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Application.ThirdPartyServices
{
    public interface IToDoAIService
    {
        Task<ToDo> GetMostImportantToDo();
    }
}
