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
    public class NoticeBoardManager : DbContextManager, IAsyncManager<NoticeBoard, int>
    {
        /// <summary>
        /// Adds/Creates a new notice board
        /// </summary>
        /// <param name="noticeBoard"></param>
        /// <returns>Returns 1 if success</returns>
        public async Task<int> AddAsync(NoticeBoard noticeBoard)
        {
            DbContext.NoticeBoards.Add(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a notice board
        /// </summary>
        /// <param name="noticeBoard"></param>
        /// <returns>Returns 1 if success</returns>
        public async Task<int> UpdateAsync(NoticeBoard noticeBoard)
        {
            DbContext.Entry(noticeBoard).State = System.Data.Entity.EntityState.Modified;
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Returns the requested NoticeBoard if available
        /// </summary>
        public async Task<NoticeBoard> GetAsync(int id)
        {
            var nb = await DbContext.NoticeBoards.FindAsync(id);
            nb.Notices = nb.Notices.Where(x => x.Status == NoticeStatus.Approved).ToList();
            return nb;
        }

        /// <summary>
        /// Returns the requested official NoticeBoard if available
        /// </summary>
        public async Task<NoticeBoard> GetOfficialBoardAsync(int id)
        {
            var nb = await DbContext.NoticeBoards.FindAsync(id);
            if (nb == null)
                return null;
            if (nb.IsOfficial)
            {
                nb.Notices = nb.Notices.Where(x => x.Status == NoticeStatus.Approved)
                    .OrderByDescending(n => n.DateCreated)
                    .ToList();
                return nb;
            }
            throw new XSException("The requested noticeboard is not an official notice board");
        }

        /// <summary>
        /// Returns the unofficial NoticeBoard if available
        /// </summary>
        public async Task<NoticeBoard> GetUnofficialBoardAsync(int id)
        {
            var nb = await DbContext.NoticeBoards.FindAsync(id);
            if (nb == null)
                return null;
            if (nb.IsOfficial)
                throw new XSException("The requested noticeboard is not an unofficial notice board");

            nb.Notices = nb.Notices.Where(x => x.Status != NoticeStatus.Flagged)
                            .OrderByDescending(n => n.DateCreated)
                            .ToList();
            return nb;
        }

        /// <summary>
        /// Returns all notice boards on XavSpace (official and unofficial)
        /// </summary>
        public async Task<IEnumerable<NoticeBoard>> GetAsync()
        {
            return await DbContext.NoticeBoards.Where(nb => !nb.IsArchived).ToListAsync();
        }

        /// <summary>
        /// Returns the official notice boards
        /// </summary>
        public async Task<IEnumerable<NoticeBoard>> GetOfficialBoardsAsync()
        {
            return await DbContext.NoticeBoards
                .Where(nb => nb.IsOfficial && !nb.IsArchived)
                .ToListAsync();
        }

        /// <summary>
        /// Returns the unofficial notice boards
        /// </summary>
        public async Task<IEnumerable<NoticeBoard>> GetUnofficialBoardsAsync()
        {
            return await DbContext.NoticeBoards
                .Where(nb => !nb.IsOfficial && !nb.IsArchived)
                .ToListAsync();
        }

        /// <summary>
        /// Deletes a notice board
        /// </summary>
        /// <param name="id">The notice board to delete</param>
        /// <returns>Returns 1 if the delete was successful</returns>
        public async Task<int> DeleteAsync(int id)
        {
            NoticeBoard noticeBoard = await DbContext.NoticeBoards.FindAsync(id);

            if (noticeBoard.Notices.Count > 0)
                throw new XSException("Notice Boards with notices cannot be deleted. Either archive the board, or delete the individual notices and then delete the board.");

            DbContext.NoticeBoards.Remove(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<List<NoticeBoard>> SearchAsync(string searchString)
        {
            var res = DbContext.NoticeBoards.Where(x => x.Title.Contains(searchString))
                .Union(DbContext.NoticeBoards.Where(y => y.Description.Contains(searchString)));

            res = res.Take(5);

            return await res.ToListAsync();
        }

        /// <summary>
        /// Deletes a notice board
        /// </summary>
        /// <param name="id">The notice board to delete</param>
        /// <returns>Returns 1 if the delete was successful</returns>
        public async Task<int> ArchiveAsync(int id)
        {
            NoticeBoard noticeBoard = await DbContext.NoticeBoards.FindAsync(id);

            if (noticeBoard.Notices.Count < 1)
                throw new XSException("The notice board needs to have at least one post in order to be archived");

            noticeBoard.Archived();
            return await UpdateAsync(noticeBoard);
        }

        public async Task<int> DeleteOrArchiveAsync(int id)
        {
            NoticeBoard noticeBoard = await DbContext.NoticeBoards.FindAsync(id);

            if (noticeBoard.IsArchived)
                throw new XSException("This notice board has already been archived");

            if (noticeBoard.Notices.Count > 0)
                return await ArchiveAsync(id);

            return await DeleteAsync(id);
        }
    }
}
