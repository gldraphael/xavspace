using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using XavSpace.Entities.Data;
using XavSpace.Entities.Relationships;
using XavSpace.Entities.Users;
using XavSpace.Facade.Managers.Base;

namespace XavSpace.Facade.Managers
{
    public class RelationshipManager : DbContextManager
    {
        /// <summary>
        /// Adds a User's noticepost relationship
        /// </summary>
        /// <param name="notice">The relationship to be added</param>
        /// <returns>1 if success</returns>
        public async Task<int> AddAsync(UserNoticePost relationship)
        {
            DbContext.UserNoticePostRelationship.Add(relationship);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a User's Noticepost relationship
        /// </summary>
        /// <param name="id">The relationship to be deleted</param>
        /// <returns>1 if success</returns>
        public async Task<int> DeleteAsync(UserNoticePost relationship)
        {
            UserNoticePost noticeBoard = await DbContext.UserNoticePostRelationship.FindAsync(relationship.UserId, relationship.NoticeId);
            DbContext.UserNoticePostRelationship.Remove(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a User's Notice Board Following relationship
        /// </summary>
        /// <param name="notice">The relationship to be added</param>
        /// <returns>1 if success</returns>
        public async Task<int> AddAsync(UserNoticeBoardFollow relationship)
        {
            DbContext.UserBoardFollowingRelationship.Add(relationship);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a User's NoticeBoard Following relationship
        /// </summary>
        /// <param name="id">The relationship to be deleted</param>
        /// <returns>1 if success</returns>
        public async Task<int> DeleteAsync(UserNoticeBoardFollow relationship)
        {
            UserNoticeBoardFollow noticeBoard = await DbContext.UserBoardFollowingRelationship.FindAsync(relationship.UserId, relationship.NoticeBoardId);
            DbContext.UserBoardFollowingRelationship.Remove(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(UserNoticeBoardFollow relationship)
        {
            var unb = await DbContext.UserBoardFollowingRelationship.FindAsync(relationship.UserId, relationship.NoticeBoardId);
            return unb != null;
        }

        public async Task<UserNoticePost> GetUserAsync(Notice notice)
        {
            var rel = await DbContext.UserNoticePostRelationship
                .Include(r=>r.User)
                .Where(x => x.NoticeId == notice.NoticeId)
                .ToListAsync();

            return rel[0];
        }
    }
}
