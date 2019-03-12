using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class Availability
    {
        public Guid Id { get; set; }
        public Coach Coach { get; set; }
        public string Name { get; set; }
        public DateTime Day { get; set; }
        public int StartHour { get; set; }
        public int EndTime { get; set; }
    }
}