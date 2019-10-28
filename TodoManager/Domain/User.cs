using System;
namespace TodoManager.Domain
{
    public class User : IAggregate<long>
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
