namespace Models;

[Serializable]
/// <summary>
/// Album DTO
/// </summary>
public class Album 
{
    /// <summary>
    /// The album's unique id
    /// </summary>
    public string AlbumId { get; set; }
    
    /// <summary>
    /// Name of the album
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Album's band
    /// </summary>
    public string Band { get; set; }
    /// <summary>
    /// A url to an image of the album cover
    /// </summary>
    public string ImageUrl { get; set; }
    /// <summary>
    /// Release date as a string. Ex: '9/20/2022'
    /// </summary>
    public string ReleaseDate { get; set; }

    /// <summary>
    /// The timestamp the album was added in milliseconds
    /// </summary>
    public long TimestampAddedMs { get; set; }
}