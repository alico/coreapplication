using CoreApplication.Data.Contracts;
using CoreApplication.Data.Contracts.Context;
using CoreApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApplication.Data
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public User Get(string userName, string password)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                return context.Users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            }
        }
    }
}
