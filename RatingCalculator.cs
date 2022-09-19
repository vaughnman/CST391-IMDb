public class RatingCalculator : IRatingCalculator
{
    private readonly IReviewDatabase _reviewDatabase;

    public RatingCalculator(IReviewDatabase reviewDatabase)
    {
        _reviewDatabase = reviewDatabase;
    }
    public async Task<double> GetRating(string albumId)
    {
        var reviews = await _reviewDatabase.Get(albumId);
        
        if(reviews == null || !reviews.Any()) return 0;

        var ratings = reviews.Select(x=>x.Rating);

        return ratings.Average();
    }
}