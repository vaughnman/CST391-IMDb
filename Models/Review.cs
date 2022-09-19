namespace Models;

[Serializable]
public class Review 
{
    public string AlbumId { get; set; }
    public string ReviewId { get; set; }
    
    
    public string Username { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public int Rating { get; set; }

    public long TimestampAddedMs { get; set; }
}