using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using XavSpace.Entities.Data;

namespace XavSpace.Website.ViewModels.Notices
{
    public class NoticeMappings
    {
        public static Notice From(NoticeViewModel vm)
        {
            Notice n = new Notice
            {
                NoticeId = vm.Id,
                Title = vm.Title,
                NoticeBoardId = vm.NoticeBoardId,
                Description = vm.Description,
                HighPriority=vm.isHighPriority
            };
            if (vm.DateCreated.HasValue)
                n.DateCreated = vm.DateCreated.Value;
            return n;
        }
        public static NoticeViewModel ToNoticeViewModel(Notice n)
        {
            NoticeViewModel vm = new NoticeViewModel
            {
                Id = n.NoticeId,
                Title = n.Title,
                NoticeBoardId = n.NoticeBoardId,
                Description = n.Description,
                DateCreated = n.DateCreated,
                isHighPriority=n.HighPriority
            };
            return vm;
        }


        public static Notice From(DetailedNoticeViewModel vm)
        {
            throw new NotImplementedException();
        }
        public static DetailedNoticeViewModel ToDetailedNoticeViewModel(Notice notice)
        {
            throw new NotImplementedException();
        }

        public static Notice From(PendingNoticeViewModel vm)
        {
            Notice n = new Notice
            {
                NoticeId = vm.Id,
                Title = vm.Title,
                NoticeBoardId = vm.NoticeBoardId,
                Description = vm.Description,
                DateCreated = vm.DateCreated,
                HighPriority = vm.IsHighPriority,
                ModeratorComment = vm.Comment,
                Status = vm.Status,
                DateReviewed = vm.DateReviewed
            };
            return n;
        }
        public static PendingNoticeViewModel ToPendingNoticeViewModel(Notice notice)
        {
            PendingNoticeViewModel vm = new PendingNoticeViewModel
            {
                Id = notice.NoticeId,
                Title = notice.Title,
                NoticeBoardId = notice.NoticeBoardId,
                Description = notice.Description,
                DateCreated = notice.DateCreated,
                IsHighPriority = notice.HighPriority,
                Comment = notice.ModeratorComment,
                Status = notice.Status,
                DateReviewed = notice.DateReviewed,
                NoticeBoardName = notice.NoticeBoard.Title
            };
            return vm;
        }

        public static T To<T>(Notice nb)
            where T : class
        {
            if (typeof(T) == typeof(NoticeViewModel))
                return ToNoticeViewModel(nb) as T;

            else if (typeof(T) == typeof(DetailedNoticeViewModel))
                return ToDetailedNoticeViewModel(nb) as T;

            else if (typeof(T) == typeof(PendingNoticeViewModel))
                return ToPendingNoticeViewModel(nb) as T;

            else
                throw new InvalidOperationException("T is not valid");
        }
    }
}
