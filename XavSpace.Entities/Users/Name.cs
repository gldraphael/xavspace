using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XavSpace.Entities.Users
{
    public class Name
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}",First, Last);
        }
    }
}
