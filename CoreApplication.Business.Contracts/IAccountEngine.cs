using CoreApplication.Business.DTO;
using System;
using System.Collections.Generic;

namespace CoreApplication.Business.Contracts
{
    public interface IAccountEngine
    {
        bool Login(LoginDTO request);
    }
}
