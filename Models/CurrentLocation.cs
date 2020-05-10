using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSL__Quiz.Models
{
    public class CurrentLocation
    {
        string name = "t5LD3v";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Distance { get; set; }
        public string Key
        {
            get { return name; }
            set { name = value; }
        }
    }
}