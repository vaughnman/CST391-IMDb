using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Moq;
using Xunit;

public class ReviewControllerTest 
{
    [Fact]
    public async Task Gets_Reviews_From_Database() 
    {
        var reviewDatabase = new Mock<IReviewDatabase>();
        var expectedReviews = new List<Review>() { new Review(), new Review() };

        reviewDatabase.Setup(x=>x.Get("Album Id")).ReturnsAsync(expectedReviews);

        var reviewController = new ReviewController(reviewDatabase.Object);

        var resultReviews = await reviewController.GetInternal("Album Id");

        Assert.Equal(expectedReviews, resultReviews);
    }

    [Fact]
    public async Task Deletes_Review_From_Database()
    {
        var reviewDatabase = new Mock<IReviewDatabase>();

        var reviewController = new ReviewController(reviewDatabase.Object);

        await reviewController.DeleteInternal("Review Id");

        reviewDatabase.Verify(x=>x.Delete("Review Id"));
    }
}