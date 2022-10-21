using System;

namespace teslacamviewer.web.Contracts
{
    public class TeslaEventContract
    {
        public DateTime? TimeStamp {get;set;}
        public string City {get;set;}
        public double Est_Lat {get;set;} 
        public double Est_Lon {get;set;}
        public string Reason {get;set;}
    }
}