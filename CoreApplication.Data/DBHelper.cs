using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CoreApplication.Data
{
    public static class DBHelper
    {
        public static T FirstOrDefaultWithNoLock<T>(this IEnumerable<T> sequence)
        {
            using (new TransactionScope(
                  TransactionScopeOption.Required,
                  new TransactionOptions
                  {
                      IsolationLevel = IsolationLevel.ReadUncommitted
                  }))
            {
                return sequence.FirstOrDefault();
            }
           
        }

        public static List<T> ToListWithNoLock<T>(this IEnumerable<T> sequence)
        {
            using (new TransactionScope(
                  TransactionScopeOption.Required,
                  new TransactionOptions
                  {
                      IsolationLevel = IsolationLevel.ReadUncommitted
                  }))
            {
                return sequence.ToList();
            }

        }
    }
}
