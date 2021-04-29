using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class EventDetails
    {
        public int EventID { get; set; }

        public int FightID { get; set; }

        public string FighterAName { get; set; }

        public string FighterBName { get; set; }

        public string EventTitle { get; set; }

        public string WinnerName { get; set; }

        public int Round { get; set; }

        public string Method { get; set; }

    }
}
