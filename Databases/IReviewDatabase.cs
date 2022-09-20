using Models;

public interface IReviewDatabase 
{
    public Task Add(Review review);
    public Task<IEnumerable<Review>> Get(string albumId);
    public Task Delete(string reviewId);
    public Task DeleteByAlbum(string albumId);
}