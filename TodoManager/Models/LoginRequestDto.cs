using System;
using System.ComponentModel.DataAnnotations;

namespace TodoManager.Models
{
    public class LoginRequestDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input username!")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input password!")]
        public string Password { get; set; }
    }
}
