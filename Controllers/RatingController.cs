using Microsoft.AspNetCore.Mvc;

namespace Controllers;

public class RatingController : IMDbController
{
    private readonly IRatingCalculator _ratingCalculator;

    public RatingController(IRatingCalculator ratingCalculator)
    {
        _ratingCalculator = ratingCalculator;
    }

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
