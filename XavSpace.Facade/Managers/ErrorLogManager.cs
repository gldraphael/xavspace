using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using XavSpace.Entities.Logs;
using XavSpace.Facade.Managers.Base;

namespace XavSpace.Facade.Managers
{
    public class ErrorLogManager : DbContextManager, IAsyncManager<ErrorLog, int>
    {
        public async Task<int> AddAsync(ErrorLog item)
        {
            DbContext.ErrorLogs.Add(item);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(ErrorLog item)
        {
            DbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
            return await DbContext.SaveChangesAsync();
        }

        public async Task<ErrorLog> GetAsync(int id)
        {
            return await DbContext.ErrorLogs.FindAsync(id);
        }

        public async Task<IEnumerable<ErrorLog>> GetAsync()
        {
            return await DbContext.ErrorLogs.ToListAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            ErrorLog log = await DbContext.ErrorLogs.FindAsync(id);
            DbContext.ErrorLogs.Remove(log);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> MarkResolved(int id)
        {
            var log = await this.GetAsync(id);
            log.Resolved = true;
            return await this.UpdateAsync(log);
        }

        public async Task<int> MarkUnresolved(int id)
        {
            var log = await this.GetAsync(id);
            log.Resolved = false;
            return await this.UpdateAsync(log);
        }
    }
}
