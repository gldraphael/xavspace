using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XavSpace.DataAccess.DbContexts;
using XavSpace.Facade.Factory;

namespace XavSpace.Facade.Managers.Base
{
    public class DbContextManager : IDisposable
    {
        protected DbContextFactory Factory { get; private set; }
        public ApplicationDbContext DbContext
        {
            get
            {
                return Factory.Context;
            }
        }

        public DbContextManager()
        {
            Factory = new DbContextFactory();
        }

        public void Dispose()
        {
            Factory.Dispose();
        }
    }
}
