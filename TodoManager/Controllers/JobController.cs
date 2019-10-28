using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain;
using TodoManager.Domain.Events;
using TodoManager.Framework.Events;
using TodoManager.Models.Enum;

namespace TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IEventBus eventBus;

        public JobController(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        [Route("todo/checkin")]
        [HttpGet]
        public Task<OkResult> CheckIn()
        {
            TodoItem todoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                Name = String.Format("Check in everyday[{0:yyyy-MMM-dd}]", DateTime.Now),
                State = (int)TodoItemStateEnum.Todo,
                Deadline = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)
            };
            Task task = eventBus.Publish(new TodoItemEvent(TodoItemEventTypeEnum.NeedToCreateNew, todoItem));
            return task.ContinueWith(t =>
            {
                return Ok();
            });
        }
    }
}
