using CoreApplication.Data.Contracts;
using CoreApplication.Data.Contracts.Context;
using CoreApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;

namespace CoreApplication.Data
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BaseDataContext context ) : base(context)
        {
        }

        public User Get(string userName, string password)
        {
            return context.Users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefaultWithNoLock();
        }
    }
}
