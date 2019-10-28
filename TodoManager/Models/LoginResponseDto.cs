using System;

namespace TodoManager.Models
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public UserDto UserInfo { get; set; }
    }
}
