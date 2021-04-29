using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WADFC.Models
{
    public class FullEventDetails
    {
        public Event EventInfo { get; set; }
        public List<EventDetails> EventDetails = new List<EventDetails>();
    }
}
