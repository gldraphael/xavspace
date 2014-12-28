using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Entities.Users
{
    public class Staff : ApplicationUser
    {
        public string Post { get; set; }
    }
}
