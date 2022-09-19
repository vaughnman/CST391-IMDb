using System.Threading.Tasks;
using Controllers;
using Moq;
using Xunit;

public class RatingControllerTest
{
    [Fact]
    public async Task Gets_Rating_From_Calculator()
    {
        var ratingCalculator = new Mock<IRatingCalculator>();
        var expectedRating = 5;

        ratingCalculator.Setup(x => x.GetRating("Some Id")).ReturnsAsync(expectedRating);

        var ratingController = new RatingController(ratingCalculator.Object);

        var resultRating = await ratingController.Get("Some Id");

        Assert.Equal(resultRating, expectedRating);
    }
}