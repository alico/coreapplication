using System;
using CoreApplication.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreApplication.DTO.RequestDTO;
using CoreApplication.Business.DTO;

namespace CoreApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private IServiceProvider _serviceProvider;
        public AccountController(IServiceProvider serviceProvider, ILogger<AccountController> logger) : base(logger)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost("login")]
        public bool Login(LoginRequestDTO loginRequestDTO)
        {
            var userEngine = _serviceProvider.GetService<IAccountEngine>();
            var loginDto = new LoginDTO()
            { 
                UserName = loginRequestDTO.UserName,
                Password = loginRequestDTO.Password
            };

            return userEngine.Login(loginDto);
        }
    }
}