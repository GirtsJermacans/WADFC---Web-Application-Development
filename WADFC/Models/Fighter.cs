using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class Fighter
    {
        [Key]
        public int FighterID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Surname { get; set; }

        [DefaultValue(0)]
        public int Win { get; set; }

        [DefaultValue(0)]
        public int Loss { get; set; }

        [DefaultValue(0)]
        public int Draw { get; set; }

        [DefaultValue(0)]
        public int NoContest { get; set; }

        [DefaultValue(0)]
        public int Height { get; set; }

        [DefaultValue(0)]
        public int Weight { get; set; }

        [DefaultValue(0)]
        public int Reach { get; set; }

        [DefaultValue("-")]
        [Column(TypeName = "varchar(20)")]
        public string Stance { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

    }
}
