using Microsoft.AspNetCore.Mvc;
using Models;


/// <summary>
/// Endpoint for all '/Review' routes
/// </summary>
public class ReviewController : IMDbController
{
    private IReviewDatabase _reviewDatabase;

    /// <summary>
    /// DI Constructor
    /// </summary>
    /// <param name="reviewDatabase">Review database to use</param>
    public ReviewController(IReviewDatabase reviewDatabase)
    {
        _reviewDatabase = reviewDatabase;
    }

    /// <summary>
    /// Gets all reviews for albumId query param
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var albumId = Request.Query["albumId"];
        
        var reviews = await GetInternal(albumId);
        
        return new OkObjectResult(reviews);
    }

    public async Task<IEnumerable<Review>> GetInternal(string albumId) 
    {
        return await _reviewDatabase.Get(albumId);
    }

    /// <summary>
    /// Deletes a review for reviewId query param
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        var reviewId = Request.Query["reviewId"];

        await DeleteInternal(reviewId);

        return Accepted();
    }

    public async Task DeleteInternal(string reviewId)
    {
        await _reviewDatabase.Delete(reviewId);
    }

    /// <summary>
    /// Adds a review to an album
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Add()
    {
        var review = await ParseBody<Review>();

        var addedReview = await AddInternal(review);

        return CreatedAtAction("Added review", addedReview.ReviewId);
    }

    public async Task<Review> AddInternal(Review review)
    {
        review.ReviewId = Guid.NewGuid().ToString();
        review.TimestampAddedMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        if(review.AlbumId == null || review.AlbumId == "")
            throw new ArgumentException("Review's album id is missing! Which album is the review for?");

        if(review.Rating > 5 || review.Rating < 0)
            throw new ArgumentException("Rating must be between 0-5!");

        await _reviewDatabase.Add(review);

        return review;
    }
}