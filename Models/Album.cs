namespace Models;

[Serializable]
public class Album 
{
    public string AlbumId { get; set; }
    
    public string Name { get; set; }
    public string Band { get; set; }
    public string ImageUrl { get; set; }
    public string ReleaseDate { get; set; }

    public long TimestampAddedMs { get; set; }
}