﻿using System;
using System.Threading.Tasks;
using MediatR;
using TodoManager.Framework.Query;

namespace TodoManager.Bus
{
    public class QueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public QueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            return _mediator.Send(query);
        }
    }
}
