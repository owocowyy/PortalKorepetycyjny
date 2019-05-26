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


        public static explicit operator int(Coach v)
        {
            throw new NotImplementedException();
        }
        public override string GetAccountType()
        {
            return "Coach";
        }
    }
}