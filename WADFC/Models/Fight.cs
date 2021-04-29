using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WADFC.Models
{
    public class Fight
    {
        [Key]
        public int FightID { get; set; }

        [ForeignKey("EventID")]
        public int? EventID { get; set; }
        public Event Event { get; set; }

        [ForeignKey("FighterA"), Column(Order = 0)]
        public virtual int? FighterAID { get; set; }

        [ForeignKey("FighterB"), Column(Order = 1)]
        public int? FighterBID { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Division { get; set; }

        [DefaultValue("-")]
        [Column(TypeName = "varchar(100)")]
        public string Winner { get; set; }

        [DefaultValue("-")]
        [Column(TypeName = "varchar(100)")]
        public string Method { get; set; }

        [DefaultValue(0)]
        public int Round { get; set; }

        public virtual Fighter FighterA { get; set; }
        public virtual Fighter FighterB { get; set; }


    }
}
