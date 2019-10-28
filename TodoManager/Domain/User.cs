using System;
using TodoManager.Framework;

namespace TodoManager.Domain
{
    public class User : IAggregate<Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
