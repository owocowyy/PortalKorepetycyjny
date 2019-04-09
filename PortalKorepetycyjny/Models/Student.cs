using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class Student : ApplicationUser
    {
        public ICollection<CoachReview> CoachReviews { get; set; }
        public ICollection<StudentAdvertisment> AdvertismentList { get; set; }
        public override string GetAccountType()
        {
            return "Student";
        }
    }
}