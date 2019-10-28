using System;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Dal.Cache;
using TodoManager.Domain.Services;
using TodoManager.Filter;
using TodoManager.Models;
using TodoManager.Models.Dto;

namespace TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginService;
        private readonly IDisctributedCacheProvider cacheProvider;

        public LoginController(LoginService loginService,
            IDisctributedCacheProvider cacheProvider)
        {
            this.loginService = loginService;
            this.cacheProvider = cacheProvider;
        }

        [HttpPost]
        public SimpleResponseDto<LoginResponseDto> SignIn([FromBody] LoginRequestDto loginRequestDto)
        {
            SimpleResponseDto<LoginResponseDto> simpleResponseDto = null;
            UserDto user = loginService.SignIn(loginRequestDto.Username, loginRequestDto.Password);
            if (user != null)
            {
                string accessToken = Guid.NewGuid().ToString("N");
                //cache accessToken and user in 30 mins 
                cacheProvider.Set(accessToken, user, DateTime.Now.AddMinutes(30));
                LoginResponseDto loginResponseDto = new LoginResponseDto();
                loginResponseDto.AccessToken = accessToken;
                loginResponseDto.UserInfo = user;
                simpleResponseDto = SimpleResponseDto<LoginResponseDto>.OK(loginResponseDto);
            }
            else
            {
                simpleResponseDto = SimpleResponseDto<LoginResponseDto>.Failed(Models.Enum.ResponseCodeEnum.ResponseCode_100);
            }
            return simpleResponseDto;
        }

        [HttpDelete]
        public SimpleResponseDto<bool> SignOut()
        {
            object accessToken = HttpContext.Items[AuthAttribute.AuthorizationHeaderName];
            if (accessToken != null)
            {
                //clear cache and other actions.
                loginService.SignOut();
                cacheProvider.Delete((string)accessToken);
            }

            return SimpleResponseDto<bool>.OK(true);
        }
    }
}
