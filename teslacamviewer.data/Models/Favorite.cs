namespace teslacamviewer.data.Models
{
    public class Favorite
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Type {get;set;}
    }

    public enum FavoriteType
    {
        Folder,
        Clip
    }
}