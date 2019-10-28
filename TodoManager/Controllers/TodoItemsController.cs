using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Bus;
using TodoManager.Domain;
using TodoManager.Domain.Commands;
using TodoManager.Framework.Querys;
using TodoManager.Models;
using TodoManager.Models.Dto;

namespace TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryBus queryBus;

        public TodoItemsController(ICommandBus commandBus, IQueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        // POST: api/todoitems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTodoItemCommand command)
        {
            command.Id = Guid.NewGuid();
            await commandBus.Send(command);

            return Created("api/todoitems", command.Id);
        }

        // GET: api/todoitems/1
        [HttpGet("{id}")]
        public async Task<SimpleResponseDto<TodoItemDto>> Get(Guid id)
        {
            Task<TodoItemDto> todoItemDto = queryBus.Send<GetTodoItemsQuery, TodoItemDto>(new GetTodoItemsQuery(id));
            return  SimpleResponseDto<TodoItemDto>.OK(todoItemDto.Result);
        }
    }
}
