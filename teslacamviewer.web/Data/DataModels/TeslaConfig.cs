using System.ComponentModel.DataAnnotations;

namespace teslacamviewer.web.Data.DataModels
{
    public class TeslaConfig
    {
        public int Id {get;set;}
        public string Password {get;set;}
        public string Salt {get;set;}
    }
}