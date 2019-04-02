using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class StudentAdvertisment
    {
        public int Id { get; set; }
        public Student Creator { get; set; }
        public Subject SbujectName { get; set; }
        public string Descryption { get; set; }
        public string Title { get; set; }
        public DateTime AdvertismentDate { get; set; }



    }
}