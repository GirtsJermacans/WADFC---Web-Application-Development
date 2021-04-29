using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class EventForm
    {
        [Key]
        public int EventID { get; set; }

        [Required(ErrorMessage = "Please enter the Event Title")]
        public string EventTitle { get; set; }

        public string Completed { get; set; }

        [Required(ErrorMessage = "Please enter where Event will take place")]
        public string Location { get; set; }

        [DefaultValue("none")]
        [Required(ErrorMessage = "Must have at least default value in it - none")]
        public string EventImage { get; set; }

        [Required(ErrorMessage = "Please pick a Date when event will take a place")]
        public DateTime Date { get; set; }
    }
}
