using System;
using System.ComponentModel.DataAnnotations;

namespace TodoManager.Domain
{
    public class TodoItem : IAggregate<long>
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input the todo's name!")]
        [StringLength(256, ErrorMessage = "The field name cann't extend 256 words!")]
        public string Name { get; set; }
        public int State { get; set; }
    }
}
