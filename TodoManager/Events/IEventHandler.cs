using MediatR;

namespace TodoManager.Events
{
    public interface IEventHandler<in TEvent>: INotificationHandler<TEvent>
           where TEvent : IEvent
    {
    }
}
