using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class AdvancedSearch
    {
        public string EventTitleString { get; set; }

        public string FighterNameString { get; set; }

        public string FighterNameStringFighter { get; set; }

        public int Win { get; set; } = -1;
 
        public int Loss { get; set; } = -1;

        public int Draw { get; set; } = -1;

        public int NoContest { get; set; } = -1;
    }
}
