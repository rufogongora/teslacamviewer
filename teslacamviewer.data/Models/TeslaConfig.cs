using System.ComponentModel.DataAnnotations;

namespace teslacamviewer.data.Models
{
    public class TeslaConfig
    {
        public int Id {get;set;}
        public string Password {get;set;}
        public string Salt {get;set;}
    }
}