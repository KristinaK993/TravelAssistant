using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistant
{
    public class Connection
    {
        public string Destination { get; set; }
        public int Distance { get; set; } // I km
        public int Time { get; set; } // I min
        public int Cost { get; set; } 
        public string TravelMethod { get; set; } // "car", "boat", "plane", etc.
    }
}
