using System;
namespace TodoManager.Models
{
    public class TodoItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public DateTime Deadline { get; set; }
    }
}
