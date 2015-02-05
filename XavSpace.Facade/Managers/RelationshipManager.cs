using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            UserNoticePost noticeBoard = await DbContext.UserNoticePostRelationship.FindAsync(relationship);
            DbContext.UserNoticePostRelationship.Remove(noticeBoard);
            return await DbContext.SaveChangesAsync();
        }
    }
}
