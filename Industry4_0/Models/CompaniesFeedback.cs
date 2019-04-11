using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Industry4_0.Models
{
    public class CompaniesFeedback
    {

        public int ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Username { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Companies { get; set; }
        [Required]
        [Range(0,5)]
        public int Rating { get; set; }
        [Required]
        public string Feedback { get; set; }
        public int Agree { get; set; }
        public int Disagree { get; set; }

        public bool AgreeAvail { get; set; }
        public bool DisagreeAvail { get; set; }

    }
}
