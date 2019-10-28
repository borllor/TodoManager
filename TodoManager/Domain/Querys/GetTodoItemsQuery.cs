using System;
using System.Collections.Generic;
using TodoManager.Framework.Query;
using TodoManager.Models;

namespace TodoManager.Framework.Querys
{
    public class GetTodoItemsQuery : IQuery<IEnumerable<TodoItemDto>>
    {
    }
}
