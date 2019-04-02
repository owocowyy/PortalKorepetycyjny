using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class AdvertisementDTO
    {
        public string Id { get; set; }
        public string CoachNameAndSurname { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}