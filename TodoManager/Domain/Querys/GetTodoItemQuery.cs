using System;
using TodoManager.Framework.Query;
using TodoManager.Models;

namespace TodoManager.Framework.Querys
{
    public class GetTodoItemQuery : IQuery<TodoItemDto>
    {
        public Guid Id { get; private set; }

        public GetTodoItemQuery(Guid id)
        {
            Id = id;
        }
    }
}
