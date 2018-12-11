using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class Coach : ApplicationUser
    {
        public ICollection<CoachReview> CoachReviews { get; set; }
    }
}