using System.Threading.Tasks;

namespace TodoManager.Framework.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}