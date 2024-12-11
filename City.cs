using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json; 
using Spectre.Console; 
using Figgle; 

namespace TravelAssistant
{
    public class City
    {
        public string Name { get; set; }
        public List<Connection> Connections { get; set; }
    }
}
