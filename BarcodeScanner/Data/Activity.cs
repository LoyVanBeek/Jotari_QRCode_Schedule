using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodeScanner.Data
{
    public class Activity
    {
        public DateTime StartTime {get; set;}

        public DateTime EndTime {get; set;}

        public string Name {get; set;}

        public string Location {get; set;}

        public Activity(string name, DateTime _start, DateTime _end, string location)
        {
            Name = name;
            StartTime = _start;
            EndTime = _end;
            Location = location;
        }

        public Activity(string name, DateTime _start, TimeSpan _duration, string location)
        {
            Name = name;
            StartTime = _start;
            EndTime = _start + _duration;
            Location = location;
        }
    }
}
