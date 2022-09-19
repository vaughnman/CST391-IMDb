using System.Collections.Concurrent;
using Models;

public class StubReviewDatabase : IReviewDatabase
{
    private static ConcurrentDictionary<string, Review> _reviews = new ConcurrentDictionary<string, Review>();

    public async Task Add(Review review)
    {
        _reviews.AddOrUpdate(review.ReviewId, (key) => review, (key, old) => review);
    }

    public async Task Delete(string reviewId)
    {
        _reviews.Remove(reviewId, out var _);
    }

    public async Task DeleteForAlbum(string albumId)
    {
        var reviewsToDelete = _reviews.Where(x=>x.Value.AlbumId == albumId).Select(x => x.Value);

         foreach(Review review in reviewsToDelete)
            _reviews.Remove(review.ReviewId, out var _);
    }

    public async Task<IEnumerable<Review>> Get(string albumId)
    {
        return _reviews.Where(x => x.Value.AlbumId == albumId).Select(x => x.Value);
    }
}