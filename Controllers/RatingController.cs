using Microsoft.AspNetCore.Mvc;

namespace Controllers;

/// <summary>
/// Endpoint for all '/Rating' routes
/// </summary>
public class RatingController : IMDbController
{
    private readonly IRatingCalculator _ratingCalculator;

    /// <summary>
    /// DI Controller
    /// </summary>
    /// <param name="ratingCalculator">Rating Calculator to use</param>
    public RatingController(IRatingCalculator ratingCalculator)
    {
        _ratingCalculator = ratingCalculator;
    }


    /// <summary>
    /// Calculates rating for albumId
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var albumId = Request.Query["albumId"];

        var rating = await Get(albumId);

        return new OkObjectResult(rating);
    }
    public async Task<double> Get(string albumId)
    {
        return await _ratingCalculator.GetRating(albumId);
    }
}
