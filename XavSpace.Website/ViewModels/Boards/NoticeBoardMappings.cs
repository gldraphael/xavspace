using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using XavSpace.Entities.Data;
using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.ViewModels.Boards
{
    public static class NoticeBoardMappings
    {
        public static NoticeBoard From(NoticeBoardViewModel vm)
        {
            throw new NotImplementedException();
        }
        public static NoticeBoardViewModel ToNoticeBoardViewModel(NoticeBoard nb)
        {
            var n = new List<NoticeViewModel>();
            nb.Notices.ForEach( x => n.Add(NoticeMappings.To<NoticeViewModel>(x)));
            NoticeBoardViewModel vm = new NoticeBoardViewModel
            {
                Id = nb.NoticeBoardId,
                Title = nb.Title,
                Description = nb.Description,
                IsOfficial = nb.IsOfficial,
                IsMandatory = nb.IsMandatory,
                Notices = n
            };
            return vm;
        }

        public static NoticeBoard From(NoticeBoardEditViewModel vm)
        {
            NoticeBoard noticeBoard = new NoticeBoard
            {
                NoticeBoardId = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                IsMandatory = vm.IsMandatory
            };
            return noticeBoard;
        }
        public static NoticeBoardEditViewModel ToNoticeBoardEditViewModel(NoticeBoard nb)
        {
            NoticeBoardEditViewModel vm = new NoticeBoardEditViewModel
            {
                Id = nb.NoticeBoardId,
                Title = nb.Title,
                Description = nb.Description,
                IsMandatory = nb.IsMandatory
            };
            return vm;
        }


        public static NoticeBoard From(NoticeBoardIndexViewModel vm)
        {
            throw new NotImplementedException();
        }
        public static NoticeBoardIndexViewModel ToNoticeBoardIndexViewModel(NoticeBoard nb)
        {
            var vm = new NoticeBoardIndexViewModel
                {
                    Id = nb.NoticeBoardId,
                    Title = nb.Title,
                    Description = nb.Description
                };
            return vm;
        }

        public static T To<T>(NoticeBoard nb)
            where T : class
        {
            if (typeof(T) == typeof(NoticeBoardIndexViewModel))
                return ToNoticeBoardIndexViewModel(nb) as T;

            else if (typeof(T) == typeof(NoticeBoardEditViewModel))
                return ToNoticeBoardEditViewModel(nb) as T;

            else if (typeof(T) == typeof(NoticeBoardViewModel))
                return ToNoticeBoardViewModel(nb) as T;

            else
                throw new InvalidOperationException("T is not valid");
        }
    }
}