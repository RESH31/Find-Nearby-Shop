using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSL__Quiz.Models
{
    public class LocationDetails
    {
        string name = "t5LD3v";
        float distance=3;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public float Distance
        {
            get { return distance; }
            set {
                    if (value<0)
                     distance = value;
                }
        }
        public string Key
        {
            get { return name; }
            set { name = value; }
        }

    }
}