using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.ThirdPartyServices;
using Tasks.Core.Domain.Entities;

namespace Tasks.Infrastructure.OpenAIServices.Implementations
{
    public class OpenAIToDoService : IToDoAIService
    {
        private readonly IToDoRepository _toDoRepository;

        public OpenAIToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<ToDo> GetMostImportantToDo()
        {
            var toDos = await _toDoRepository.GetToDosAsync(false);

            // go to chat gpt or something and ask it whats the most important

            return await Task.FromResult<ToDo>(toDos.FirstOrDefault());
        }
    }
}
