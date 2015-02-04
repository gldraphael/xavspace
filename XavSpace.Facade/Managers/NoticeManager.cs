using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using XavSpace.Entities.Data;
using XavSpace.Facade.Managers.Base;

namespace XavSpace.Facade.Managers
{
    public class NoticeManager : DbContextManager, IAsyncManager<Notice, int>
    {
        /// <summary>
        /// Adds a notice to the database
        /// </summary>
        /// <param name="notice">The notice to be added</param>
        /// <returns>1 if success</returns>
        public async Task<int> AddAsync(Notice notice)
        {
            using (var nbm = new NoticeBoardManager())
            {
                var nb = await nbm.GetAsync(notice.NoticeBoardId);
                if (!nb.IsOfficial)
                    notice.Approve();                   // unofficial notices are approved by default
            }

            DbContext.Notices.Add(notice);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the notice
        /// </summary>
        /// <param name="notice">The notice to be updated</param>
        /// <returns>1 if success</returns>
        public async Task<int> UpdateAsync(Notice notice)
        {
            DbContext.Entry(notice).State = System.Data.Entity.EntityState.Modified;
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Returns the specified notice
        /// </summary>
        /// <param name="id">The id of the notice</param>
        /// <returns>Returns the requested notice. Returns null if the notice doesn't exist.</returns>
        public async Task<Notice> GetAsync(int id)
        {
            return await DbContext.Notices.FindAsync(id);
        }

        /// <summary>
        /// Returns all the approved notices on all notice boards
        /// </summary>
        public async Task<IEnumerable<Notice>> GetAsync()
        {
            return await GetAsync(false);
        }

        /// <summary>
        /// Returns all the approved notices and the corresponding noticeboard details for all notice boards
        /// </summary>
        public async Task<IEnumerable<Notice>> GetDetailedAsync()
        {
            return await DbContext.Notices
                .Where(x => x.Status == NoticeStatus.Approved)
                .Include(nb => nb.NoticeBoard)
                .ToListAsync();
        }

        /// <summary>
        /// Returns all the approved notices on all notice boards
        /// </summary>
        /// <param name="sortDescendingByDate">Sorts latest first, if true</param>
        /// <returns></returns>
        public async Task<IEnumerable<Notice>> GetAsync(bool sortDescendingByDate)
        {
            var notices = DbContext.Notices
                .Where(x => x.Status == NoticeStatus.Approved);

            notices = notices.OrderByDescending(n => n.DateCreated);

            return await notices.ToListAsync();
        }

        /// <summary>
        /// Returns all of the pending notices for all official notice boards in common
        /// </summary>
        public async Task<IEnumerable<Notice>> GetPendingAsync()
        {
            var l = from x in DbContext.Notices
                    where x.NoticeBoard.IsOfficial && x.Status == NoticeStatus.PendingApproval
                    select x;
            return await l.Include(n => n.NoticeBoard).ToListAsync();
        }

        /// <summary>
        /// Returns all notices on all notice boards
        /// </summary>
        public async Task<IEnumerable<Notice>> GetAllAsync()
        {
            return await DbContext.Notices.ToListAsync();
        }

        /// <summary>
        /// Deletes a notice
        /// </summary>
        /// <param name="id">The id of the notice to be deleted</param>
        /// <returns>1 if success</returns>
        public async Task<int> DeleteAsync(int id)
        {
            Notice noticeBoard = await DbContext.Notices.FindAsync(id);
            DbContext.Notices.Remove(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Approves the notice
        /// </summary>
        /// <param name="id">The id of the notice to be approved</param>
        /// <returns>1 if success</returns>
        public async Task<int> Approve(int id)
        {
            var n = await this.GetAsync(id);
            n.Approve();
            return await this.UpdateAsync(n);
        }

        /// <summary>
        /// Declines the notice
        /// </summary>
        /// <param name="id">The id of the notice to be declined</param>
        /// <returns>1 if success</returns>
        public async Task<int> Disapprove(int id)
        {
            var n = await this.GetAsync(id);
            n.Disapprove();
            return await this.UpdateAsync(n);
        }
    }
}
