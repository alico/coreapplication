﻿using CoreApplication.Business.Contracts;
using CoreApplication.Common;
using CoreApplication.Data.Contracts;
using CoreApplication.Data.Entity;
using CoreApplication.DTO;
using CoreApplication.Business.DTO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreApplication.Business
{
    public class AccountEngine : IAccountEngine
    {
        IServiceProvider _serviceProvider; 

        public AccountEngine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public UserDTO Login(LoginDTO loginDTO)
        {
            var userRepository = _serviceProvider.GetService<IUserRepository>();
            var user = userRepository.Get(loginDTO.UserName, loginDTO.Password);

            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Status = user.Status,
                Surname = user.Surname,
                UserName = user.UserName
            };
        }
    }
}
