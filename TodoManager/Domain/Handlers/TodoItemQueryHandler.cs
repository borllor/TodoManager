using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoManager.Dal;
using TodoManager.Domain;
using TodoManager.Framework.Query;
using TodoManager.Framework.Querys;
using TodoManager.Models;

namespace TodoManager.Framework.Handlers
{
    public class TodoItemQueryHandler : IQueryHandler<GetTodoItemQuery, TodoItemDto>, IQueryHandler<GetTodoItemsQuery, IEnumerable<TodoItemDto>>
    {
        private readonly TodoItemContext todoItemContext;

        public TodoItemQueryHandler(TodoItemContext todoItemContext)
        {
            this.todoItemContext = todoItemContext;
        }

        public Task<TodoItemDto> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
        {
            Task<TodoItem> todoItem = todoItemContext.TodoItem
                .FirstOrDefaultAsync(t => request.Id == t.Id, cancellationToken);
            return todoItem.ContinueWith<TodoItemDto>(t =>
            {
                TodoItem item = t.Result;
                if (item != null)
                {
                    return ConvertToDto(item);
                }
                return null;
            });
        }

        public Task<IEnumerable<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
        {
            Task<List<TodoItem>> todoItems = todoItemContext.TodoItem.ToListAsync();
            return todoItems.ContinueWith<IEnumerable<TodoItemDto>>(t =>
            {
                List<TodoItem> items = t.Result;
                if (items != null)
                {
                    List<TodoItemDto> itemDtos = new List<TodoItemDto>(items.Count);

                    items.ForEach(it =>
                    {
                        itemDtos.Add(ConvertToDto(it));
                    });
                    return itemDtos;
                }
                return null;
            });
        }

        private TodoItemDto ConvertToDto(TodoItem item)
        {
            return new TodoItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                State = item.State,
                Deadline = item.Deadline
            };
        }
    }
}
