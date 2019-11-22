using CoreApplication.Business.Contracts;
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

        public bool Login(LoginDTO loginDTO)
        {
            var user = _serviceProvider.GetService<IUserRepository>();  


            return user != null;
        }
    }
}
