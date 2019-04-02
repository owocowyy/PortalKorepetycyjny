﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class Advertisment
    {
        public int Id { get; set; }
        public string CoachId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }

        public virtual Coach Coach { get; set; }
    }
}