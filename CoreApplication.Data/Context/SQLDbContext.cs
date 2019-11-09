using CoreApplication.Data.Contracts.Context;
using CoreApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data
{
    public class SQLDbContext : BaseDataContext, ISQLDbContext
    {
        public SQLDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
