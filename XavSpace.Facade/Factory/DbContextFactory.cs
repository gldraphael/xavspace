using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using XavSpace.DataAccess.DbContexts;

namespace XavSpace.Facade.Factory
{
    public class DbContextFactory : IDisposable
    {
        private static int ref_count = 0;
        private static ApplicationDbContext context = null;

        public DbContextFactory()
        {
            context = new ApplicationDbContext();
            Interlocked.Increment(ref ref_count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// using(var context = new DbContextFactory().Context)
        /// {
        ///     // use context here
        /// }
        /// </example>
        public ApplicationDbContext Context
        {
            get
            {
                if (ref_count == 0)
                {
                    context = new ApplicationDbContext();
                }
                Interlocked.Increment(ref ref_count);
                return context;
            }
        }

        public void Dispose()
        {
            Interlocked.Decrement(ref ref_count);
            if (ref_count == 0)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}
