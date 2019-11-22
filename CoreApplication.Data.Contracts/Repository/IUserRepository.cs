using CoreApplication.Data.Entity;
using System;
using System.Collections.Generic;

namespace CoreApplication.Data.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User Get(string userName, string password);
    }
}
