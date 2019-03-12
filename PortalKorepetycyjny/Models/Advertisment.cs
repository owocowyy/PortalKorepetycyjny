using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class Advertisment
    {
        public Guid Id { get; set; }
        public Coach CoachId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }

    }
}