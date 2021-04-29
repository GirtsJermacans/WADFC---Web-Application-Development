using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class FightDetails
    {
        public int EventID { get; set; }
        public int FightID { get; set; }
        public string Winner { get; set; }
        public string Method { get; set; }
        public string Divisions { get; set; }
        public int Round { get; set; }
        public Fighter FighterA { get; set; }
        public Fighter FighterB { get; set; }
        public List<Fighter> FightersList { get; set; }
        public FightForm NewFightForm { get; set; }
    }
}
