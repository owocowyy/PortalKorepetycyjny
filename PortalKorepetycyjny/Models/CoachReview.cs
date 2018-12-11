using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalKorepetycyjny.Models
{
    public class CoachReview
    {
        public int Id { get; set; }

        [Range(1, 5)]
        [Required]
        public int Rating { get; set; }

        [Required]
        [StringLength(1024)]
        public string Body { get; set; }

        public virtual Coach Coach { get; set; }
        public virtual Student Student { get; set; }
    }
}