using System;
using TodoManager.Domain;
using TodoManager.Framework.Command;

namespace TodoManager.Domain.Commands
{
    public class CreateTodoItemCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public DateTime Deadline { get; set; }
    }
}
