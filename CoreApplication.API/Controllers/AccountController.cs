using System;
using CoreApplication.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreApplication.DTO.RequestDTO;
using CoreApplication.Business.DTO;
using CoreApplication.DTO;

namespace CoreApplication.API.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        private IServiceProvider _serviceProvider;
        public AccountController(IServiceProvider serviceProvider, ILogger<AccountController> logger) : base(logger)
        {
            _serviceProvider = serviceProvider;
        }


        [HttpPost("login")]
        public UserResponseDTO Login(LoginRequestDTO request)
        {
            var accountEngine = _serviceProvider.GetService<IAccountEngine>();
            var loginDto = new LoginDTO()
            { 
                UserName = request.UserName,
                Password = request.Password
            };

            var user = accountEngine.Login(loginDto);

            var response = new UserResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Status = user.Status,
                Surname = user.Surname,
                UserName = user.UserName
            };

            return response;
        }

        [HttpPost("create")]
        public UserResponseDTO Create(AccountCreateRequestDTO request)
        {
            var accountEngine = _serviceProvider.GetService<IAccountEngine>();

            var userDTO = new UserDTO()
            {
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname

            };

            var user = accountEngine.Create(userDTO);

            var response = new UserResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Status = user.Status,
                Surname = user.Surname,
                UserName = user.UserName
            };

            return response;
        }
    }
}