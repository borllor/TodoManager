using System;
using System.ComponentModel.DataAnnotations;
using TodoManager.Domain;
using TodoManager.Framework.Command;

namespace TodoManager.Domain.Commands
{
    public class UpdateTodoItemCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public DateTime Deadline { get; set; }
    }
}
