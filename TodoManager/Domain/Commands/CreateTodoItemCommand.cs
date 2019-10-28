using System;
using System.ComponentModel.DataAnnotations;
using TodoManager.Domain;
using TodoManager.Framework.Command;

namespace TodoManager.Domain.Commands
{
    public class CreateTodoItemCommand : ICommand
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input the todo's name!")]
        [StringLength(256, ErrorMessage = "The field name cann't extend 256 words!")]
        public string Name { get; set; }
        public int State { get; set; }
        public DateTime Deadline { get; set; }
    }
}
