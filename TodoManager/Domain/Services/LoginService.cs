using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TodoManager.Dal;
using TodoManager.Models;
using TodoManager.Utility;

namespace TodoManager.Domain.Services
{
    public class LoginService
    {
        private IServiceProvider serviceProvider;

        public LoginService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public UserDto SignIn(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                     string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            User user = GetUserByUsername(username);
            if (user == null && "admin".Equals(username))
            {
                // init admin user
                InitUser("admin", MD5Helper.ToMD5Hash("123456"));
            }
            user = GetUserByUsername(username);
            if (user.Password.Equals(password.ToMD5Hash()))
            {
                return new UserDto() { Id = user.Id, Username = user.Username };
            }
            return null;
        }

        public void SignOut()
        {
            //TODO record user logs
        }

        public void InitUser(string username, string password)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var timeManagerContext = serviceScope.ServiceProvider.GetService<TodoItemContext>();
                User user = new User()
                {
                    Username = username,
                    Password = password
                };
                timeManagerContext.User.Update(user);
                timeManagerContext.SaveChanges();
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var timeManagerContext = serviceScope.ServiceProvider.GetService<TodoItemContext>();
                User user = timeManagerContext.User.FirstOrDefault(s => s.Username == username);
                return user;
            }
        }
    }
}
