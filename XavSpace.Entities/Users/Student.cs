using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Entities.Users
{
    public class Student : ApplicationUser
    {
        /// <summary>
        /// The year in which the student is studying
        /// </summary>
        public Year? Year { get; set; }

        public string Class { get; set; }
    }
}
