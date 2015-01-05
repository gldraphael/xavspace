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
                Description = vm.Description
            };
            return n;
        }
        public static NoticeViewModel ToNoticeViewModel(Notice n)
        {
            NoticeViewModel vm = new NoticeViewModel
            {
                Id = n.NoticeId,
                Title = n.Title,
                NoticeBoardId = n.NoticeBoardId,
                Description = n.Description
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

        public static T To<T>(Notice nb)
            where T : class
        {
            if (typeof(T) == typeof(NoticeViewModel))
                return ToNoticeViewModel(nb) as T;

            else if (typeof(T) == typeof(DetailedNoticeViewModel))
                return ToDetailedNoticeViewModel(nb) as T;

            else
                throw new InvalidOperationException("T is not valid");
        }
    }
}
