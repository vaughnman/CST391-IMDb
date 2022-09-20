namespace Models;

[Serializable]
/// <summary>
/// Review DTO
/// </summary>
public class Review 
{
    /// <summary>
    /// The id of the album the review is for
    /// </summary>
    public string AlbumId { get; set; }
    /// <summary>
    /// This review's unique id
    /// </summary>
    public string ReviewId { get; set; }
    
    /// <summary>
    /// The posting user's name
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// The review's Title
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// The review's content
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// The user's album rating from 0-5
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Time the review was added at.
    /// </summary>
    public long TimestampAddedMs { get; set; }
}