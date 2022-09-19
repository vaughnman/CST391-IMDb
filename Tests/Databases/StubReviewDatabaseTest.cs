using System.Linq;
using System.Threading.Tasks;
using Models;
using Xunit;

public class StubReviewDatabaseTest 
{

    [Fact]
    public async Task Can_Add_Review_Then_Lookup_By_Album_Id()
    {
        var reviewDatabase = new StubReviewDatabase();
        
        var review = new Review() { ReviewId = "Review Id 1", AlbumId = "Album Id 10" };

        await reviewDatabase.Add(review);

        var reviews = await reviewDatabase.Get("Album Id 10");

        Assert.Equal(1, reviews.Count());
    }

    [Fact]
    public async Task Updates_If_Matching_Review_Id()
    {
        var reviewDatabase = new StubReviewDatabase();
        
        var review = new Review() { ReviewId = "Review Id 2", AlbumId = "Album Id 11", Title = "Original Title" };
        var overwrittenReview = new Review() { ReviewId = "Review Id 2", AlbumId = "Album Id 11", Title = "Overwritten" };

        await reviewDatabase.Add(review);
        await reviewDatabase.Add(overwrittenReview);

        var reviews = await reviewDatabase.Get("Album Id 11");

        Assert.Equal(1, reviews.Count());
        Assert.Equal("Overwritten", reviews.First().Title);
    }

    [Fact]
    public async Task Deletes_By_Review_Id()
    {
        var reviewDatabase = new StubReviewDatabase();
        
        var review = new Review() { ReviewId = "Review Id 3", AlbumId = "Album Id 12" };

        await reviewDatabase.Add(review);
        await reviewDatabase.Delete(review.ReviewId);

        var reviews = await reviewDatabase.Get("Album Id 12");

        Assert.Equal(0, reviews.Count());
    }

    [Fact]
    public async Task Deletes_By_Album()
    {
        var reviewDatabase = new StubReviewDatabase();
        
        var review1 = new Review() { ReviewId = "Review Id 4", AlbumId = "Album Id 13" };
        var review2 = new Review() { ReviewId = "Review Id 5", AlbumId = "Album Id 13" };

        await reviewDatabase.Add(review1);
        await reviewDatabase.Add(review2);
        await reviewDatabase.DeleteForAlbum(review1.AlbumId);

        var reviews = await reviewDatabase.Get("Album Id 13");

        Assert.Equal(0, reviews.Count());

    }
}