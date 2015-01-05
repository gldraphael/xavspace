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
        public async Task<int> AddAsync(NoticeBoard noticeBoard)
        {
            DbContext.NoticeBoards.Add(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(NoticeBoard noticeBoard)
        {
            DbContext.Entry(noticeBoard).State = System.Data.Entity.EntityState.Modified;
            return await DbContext.SaveChangesAsync();
        }

        public async Task<NoticeBoard> GetAsync(int id)
        {
            return await DbContext.NoticeBoards.FindAsync(id);
        }

        public async Task<IEnumerable<NoticeBoard>> GetAsync()
        {
            return await DbContext.NoticeBoards.ToListAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            NoticeBoard noticeBoard = await DbContext.NoticeBoards.FindAsync(id);
            DbContext.NoticeBoards.Remove(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> PostNotice(Notice notice)
        {
            DbContext.Notices.Add(notice);
            return await DbContext.SaveChangesAsync();
        }
    }
}
