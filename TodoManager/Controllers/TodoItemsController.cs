using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Bus;
using TodoManager.Domain.Commands;
using TodoManager.Filter;
using TodoManager.Framework.Querys;
using TodoManager.Models;
using TodoManager.Models.Dto;
using TodoManager.Models.Enum;

namespace TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Auth]
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

        // PUT: api/todoitems/{id}
        [HttpPut("{id}")]
        public Task<SimpleResponseDto<bool>> Put(Guid id, [FromBody] UpdateTodoItemCommand command)
        {
            command.Id = id;
            Task task = commandBus.Send(command);
            return task.ContinueWith(t =>
            {
                return SimpleResponseDto<bool>.OK(true);
            });
        }

        // PUT api/todoitems/1
        [HttpPut("{id}/{state}")]
        public Task<SimpleResponseDto<bool>> ChangeState(Guid id, string state)
        {
            Object obj = Enum.Parse(typeof(TodoItemStateEnum), state, true);
            if (obj != null)
            {
                TodoItemStateEnum todoItemStateEnum = (TodoItemStateEnum)obj;

                Task task = commandBus.Send(new ChangeStateOfTodoItemCommand(id, (int)todoItemStateEnum));
                return task.ContinueWith(t =>
                {
                    return SimpleResponseDto<bool>.OK(true);
                });
            }

            return new Task<SimpleResponseDto<bool>>(() => { return SimpleResponseDto<bool>.Failed(ResponseCodeEnum.ResponseCode_100); });
        }

        // GET: api/todoitems
        [HttpGet]
        public Task<SimpleResponseDto<IEnumerable<TodoItemDto>>> GetList()
        {
            Task<IEnumerable<TodoItemDto>> todoItemDtos = queryBus.Send<GetTodoItemsQuery, IEnumerable<TodoItemDto>>(new GetTodoItemsQuery());
            return todoItemDtos.ContinueWith(t =>
            {
                return SimpleResponseDto<IEnumerable<TodoItemDto>>.OK(todoItemDtos.Result);
            });
        }

        // GET: api/todoitems/1
        [HttpGet("{id}")]
        public Task<SimpleResponseDto<TodoItemDto>> Get(Guid id)
        {
            Task<TodoItemDto> todoItemDto = queryBus.Send<GetTodoItemQuery, TodoItemDto>(new GetTodoItemQuery(id));
            return todoItemDto.ContinueWith(t =>
            {
                return SimpleResponseDto<TodoItemDto>.OK(todoItemDto.Result);
            });
        }
    }
}
