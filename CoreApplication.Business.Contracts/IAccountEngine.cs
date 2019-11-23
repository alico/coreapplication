using CoreApplication.Business.DTO;
using System;
using System.Collections.Generic;

namespace CoreApplication.Business.Contracts
{
    public interface IAccountEngine
    {
        UserDTO Login(LoginDTO request);
        UserDTO Create(UserDTO request);
    }
}
