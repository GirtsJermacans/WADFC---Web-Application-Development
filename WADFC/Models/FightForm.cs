using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class FightForm
    {
            
        [Key]
        [ReadOnly(true)]
        public int FightID { get; set; }

        public int EventID { get; set; }

        [Required(ErrorMessage = "Please select first fighter")]
        public int FighterAID { get; set; }

        [Required(ErrorMessage = "Please select second fighter")]
        public int FighterBID { get; set; }

        [Required(ErrorMessage = "Please enter the weight class")]
        public string Division { get; set; }

        [Required(ErrorMessage = "Who won the fight?")]
        public string Winner { get; set; }

        [Required(ErrorMessage = "How the fight was won?")]
        public string Method { get; set; }

        [Range(1, 5, ErrorMessage = "Min value = 1, Max value = 5")]
        public int Round { get; set; }
    }
}
