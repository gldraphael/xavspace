using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Entities.Logs
{
    /// <summary>
    /// Represents an error log
    /// </summary>
    public class ErrorLog
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime TimeStampUtc { get; set; }

        public string Name { get; set; }
        public string Message { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }

        public ErrorLog()
        {
            StatusCode = 500;
            TimeStampUtc = DateTime.UtcNow;
        }
    }
}
