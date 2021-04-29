using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class FighterForm
    {
        [Key]
        public int FighterID { get; set; }

        [Required(ErrorMessage = "Please enter fighter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter fighter Surname")]
        public string Surname { get; set; }

        [Range(0, 1000, ErrorMessage = "Min value = 0")]
        public int Win { get; set; }

        [Range(0, 1000, ErrorMessage = "Min value = 0")]
        public int Loss { get; set; }

        [Range(0, 1000, ErrorMessage = "Min value = 0")]
        public int Draw { get; set; }

        [Range(0, 1000, ErrorMessage = "Min value = 0")]
        public int NoContest { get; set; }

        [Range(100, 300, ErrorMessage = "Min value = 100, Max value = 300")]
        public int Height { get; set; }

        [Range(45, 120, ErrorMessage = "Min value = 45, Max value = 120")]
        public int Weight { get; set; }

        [Range(35, 100, ErrorMessage = "Min value = 35, Max value = 100")]
        public int Reach { get; set; }

        public string Stance { get; set; }

        [Required(ErrorMessage = "Please pick a Date when event will take a place")]
        public DateTime DOB { get; set; }

    }
}
