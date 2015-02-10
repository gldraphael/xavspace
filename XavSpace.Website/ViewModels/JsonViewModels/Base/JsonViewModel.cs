using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XavSpace.Website.ViewModels.JsonViewModels.Base
{
    public class JsonViewModel
    {
        public string status { get; set; }

        public static JsonViewModel Success
        {
            get
            {
                return new JsonViewModel
                {
                    status = "success"
                };
            }
        }

        public static JsonViewModel Error
        {
            get
            {
                return new JsonViewModel
                {
                    status = "error"
                };
            }
        }
    }
}