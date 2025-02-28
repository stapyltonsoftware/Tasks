using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Core.Application.Features.ToDos.AddToDo;
using Tasks.Core.Application.Features.ToDos.GetToDos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tasks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddToDoCommand> _addValidator;

        public ToDosController(IMediator mediator,
            IValidator<AddToDoCommand> addValidator)
        {
            _mediator = mediator;
            _addValidator = addValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _mediator.Send(new GetToDosQuery());

                return Ok(response.ToDos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddToDoCommand command)
        {
            try
            {
                var validationResult = await _addValidator.ValidateAsync(command);

                if (!validationResult.IsValid) 
                {
                    return BadRequest(validationResult.Errors);
                }

                var response = await _mediator.Send(command);

                return Ok(response.ToDo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
