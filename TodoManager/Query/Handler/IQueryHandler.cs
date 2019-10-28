using System;
using MediatR;
using TodoManager.Bus;

namespace TodoManager.Query.Handler
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
           where TQuery : IQuery<TResponse>
    {
    }
}
