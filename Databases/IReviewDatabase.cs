using Models;

/// <summary>
/// Review Database interface
/// </summary>
public interface IReviewDatabase 
{
    /// <summary>
    /// Add review to album
    /// </summary>
    public Task Add(Review review);
    /// <summary>
    /// Get reviews for an album
    /// </summary>
    public Task<IEnumerable<Review>> Get(string albumId);
    /// <summary>
    /// Deletes review
    /// </summary>
    public Task Delete(string reviewId);
    /// <summary>
    /// Deletes all reviews for an album
    /// </summary>
    public Task DeleteByAlbum(string albumId);
}