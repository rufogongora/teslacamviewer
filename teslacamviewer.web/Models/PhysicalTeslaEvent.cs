using System;

namespace teslacamviewer.web.Models
{
    public class PhysicalTeslaEvent
    {
        public DateTime? TimeStamp {get;set;}
        public string City {get;set;}
        public double Est_Lat {get;set;} 
        public double Est_Lon {get;set;}
        public string Reason {get;set;}

    }
}