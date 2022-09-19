using System;
using System.Threading.Tasks;
using Models;
using Moq;
using Xunit;

public class ReviewController_Add_Test
{
    [Fact]
    public async Task Adds_Review_To_Database()
    {
        var reviewDatabase = new Mock<IReviewDatabase>();
        var expectedReview = new Review() { AlbumId = "Some Album" };

        var reviewController = new ReviewController(reviewDatabase.Object);

        await reviewController.AddInternal(expectedReview);
        
        reviewDatabase.Verify(x => x.Add(expectedReview));
    }

    [Fact]
    public async Task Throws_If_Rating_Is_Out_Of_Bounds()
    {
        var reviewDatabase = new Mock<IReviewDatabase>();

        var reviewController = new ReviewController(reviewDatabase.Object);

        await Assert.ThrowsAnyAsync<Exception>(
            async () => await reviewController.AddInternal(new Review() { Rating = -1, AlbumId = "1" }));

        await Assert.ThrowsAnyAsync<Exception>(
            async () => await reviewController.AddInternal(new Review() { Rating = 6, AlbumId = "1"}));
    }

    [Fact]
    public async Task Throws_If_AlbumId_Is_Nonexistent()
    {
        var reviewDatabase = new Mock<IReviewDatabase>();

        var reviewController = new ReviewController(reviewDatabase.Object);

        await Assert.ThrowsAnyAsync<Exception>(
            async () => await reviewController.AddInternal(new Review() { AlbumId = null }));
            
        await Assert.ThrowsAnyAsync<Exception>(
            async () => await reviewController.AddInternal(new Review() { AlbumId = "" }));
    }

    [Fact]
    public async Task Initializes_Review_Id()
    {
        var reviewDatabase = new Mock<IReviewDatabase>();

        var reviewController = new ReviewController(reviewDatabase.Object);

        await reviewController.AddInternal(new Review() { AlbumId = "1", ReviewId = "Original" });
        
        reviewDatabase.Verify(x => x.Add(It.Is<Review>(
            x => x.ReviewId != null && x.ReviewId != "Original"
        )));
    }

    [Fact]
    public async Task Initializes_Timestamp_Added_Ms() 
    {
        var reviewDatabase = new Mock<IReviewDatabase>();

        var reviewController = new ReviewController(reviewDatabase.Object);

        await reviewController.AddInternal(new Review() { AlbumId = "1", TimestampAddedMs = -1 });
        
        var currentTimeMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        reviewDatabase.Verify(x => x.Add(It.Is<Review>(
            x => currentTimeMs - x.TimestampAddedMs < 1000
        )));
    }
}