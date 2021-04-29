using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class EventFighters
    {
        public int EventID { get; set; }
        public List<Fighter> FightersList { get; set; }
        public FightForm NewFightForm { get; set; }
    }
}
