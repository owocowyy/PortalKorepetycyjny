using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class Coach : ApplicationUser
    {
        public ICollection<CoachReview> CoachReviews { get; set; }

        public ICollection<Advertisment> Advertisments { get; set; }

        public ICollection<Subject> Subjects { get; set; }


        public string Name { get; set; }
        public string Surname { get; set; }

    }
}