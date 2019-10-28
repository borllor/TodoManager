using System;
using TodoManager.Framework.Query;
using TodoManager.Models;

namespace TodoManager.Framework.Querys
{
    public class GetTodoItemsQuery : IQuery<TodoItemDto>
    {
        public Guid Id { get; private set; }

        public GetTodoItemsQuery(Guid id)
        {
            Id = id;
        }
    }
}
