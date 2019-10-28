using System;
using System.Threading.Tasks;
using MediatR;

namespace TodoManager.Bus
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
