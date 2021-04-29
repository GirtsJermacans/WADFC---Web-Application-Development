using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class FighterDetails
    {
        public int FightID { get; set; }

        public string FighterAName { get; set; }

        public string FighterBName { get; set; }

        public string EventTitle { get; set; }

        public string WinnerName { get; set; }

        public int Round { get; set; }

        public string Method { get; set; }

        public string MainFighterName { get; set; }

        public string Record { get; set; }

        public string Characteristics { get; set; }

        public string Stance { get; set; }
    }
}
