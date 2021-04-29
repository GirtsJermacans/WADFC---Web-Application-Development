using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string EventTitle { get; set; }

        [Required]
        [Column(TypeName = "varchar(2)")]
        public string Completed { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string EventImage { get; set; }

    }
}
