using System;
using TodoManager.Domain;

namespace TodoManager.Command
{
    public class CreateTodoItemCommand : ICommand
    {
        public TodoItem TodoItem { get; set; }
    }
}
