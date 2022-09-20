/// <summary>
/// A calculator that determines an album's rating 
/// </summary>
public class RatingCalculator : IRatingCalculator
{
    private readonly IReviewDatabase _reviewDatabase;

    /// <summary>
    /// DI Constructor
    /// </summary>
    public RatingCalculator(IReviewDatabase reviewDatabase)
    {
        _reviewDatabase = reviewDatabase;
    }

    /// <summary>
    /// Gets an album's rating by averaging all reviews
    /// </summary>
    public async Task<double> GetRating(string albumId)
    {
        var reviews = await _reviewDatabase.Get(albumId);
        
        if(reviews == null || !reviews.Any()) return 0;

        var ratings = reviews.Select(x=>x.Rating);

        return ratings.Average();
    }
}