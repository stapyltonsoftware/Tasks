using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Application.Exceptions;
using Tasks.Core.Application.Features.ToDos.AddToDo;
using Tasks.Core.Application.Features.ToDos.CompleteToDo;
using Tasks.Core.Application.Features.ToDos.DeleteToDo;
using Tasks.Core.Application.Features.ToDos.GetToDo;
using Tasks.Core.Application.Features.ToDos.GetToDos;
using Tasks.Core.Application.Features.ToDos.UpdateToDo;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tasks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddToDoCommand> _addValidator;
        private readonly IValidator<CompleteToDoCommand> _completeValidator;
        private readonly IValidator<GetToDoQuery> _getToDoValidator;
        private readonly IValidator<DeleteToDoCommand> _deleteValidator;
        private readonly IValidator<UpdateToDoCommand> _updateValidator;

        public ToDosController(IMediator mediator,
            IValidator<AddToDoCommand> addValidator,
            IValidator<CompleteToDoCommand> completeValidator,
            IValidator<GetToDoQuery> getToDoValidator,
            IValidator<DeleteToDoCommand> deleteValidator,
            IValidator<UpdateToDoCommand> updateValidator)
        {
            _mediator = mediator;
            _addValidator = addValidator;
            _completeValidator = completeValidator;
            _getToDoValidator = getToDoValidator;
            _deleteValidator = deleteValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "", bool includeCompleted = false)
        {
            try
            {
                var response = await _mediator.Send(new GetToDosQuery() { Search = search, IncludeCompleted = includeCompleted });

                return Ok(response.ToDos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{toDoId}")]
        public async Task<IActionResult> Get(int toDoId)
        {
            try
            {
                var query = new GetToDoQuery { ToDoId = toDoId };

                var validationResult = await _getToDoValidator.ValidateAsync(query);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var response = await _mediator.Send(query);

                return Ok(response.ToDo);
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (Exception ex)
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

        [HttpDelete("{toDoId}")]
        public async Task<IActionResult> Delete(int toDoId)
        {
            try
            {
                var command = new DeleteToDoCommand { ToDoId = toDoId };

                var validationResult = await _deleteValidator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                await _mediator.Send(command);

                return Ok();
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{toDoId}")]
        public async Task<IActionResult> Update(int toDoId, UpdateToDoCommand command)
        {
            try
            {
                if (command.ToDoId != toDoId)
                {
                    return BadRequest($"Route ToDoId and Body ToDoId do not match");
                }

                var validationResult = await _updateValidator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                return Ok(await _mediator.Send(command));   
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{toDoId}/complete")]
        public async Task<IActionResult> Complete(int toDoId)
        {
            try
            {
                var command = new CompleteToDoCommand { ToDoId = toDoId };
                var validationResult = await _completeValidator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                await _mediator.Send(command);

                return Ok();
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (InvalidOperationException ivdEx)
            {
                return Conflict(ivdEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
