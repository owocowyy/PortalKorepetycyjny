using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class Status
    {
        public int Id { get; set; }
        public Coach CoachName { get; set; }
        public string StatusName { get; set; }
        public DateTime LastActivity { get; set; }

    }
}