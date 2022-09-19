using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Moq;
using Xunit;

public class RatingCalculatorTest 
{
    [Fact]
    public async Task Retrieves_All_Reviews_And_Returns_Average()
    {
        var reviewDatabase = new Mock<IReviewDatabase>();
        
        var expectedReviews = new List<Review>() {
            new Review() { Rating = 1 }, 
            new Review() { Rating = 3 }, 
            new Review() { Rating = 5 },
        };

        reviewDatabase.Setup(x=>x.Get("Some Album Id")).ReturnsAsync(expectedReviews);


        var ratingCalculator = new RatingCalculator(reviewDatabase.Object);

        var rating = await ratingCalculator.GetRating("Some Album Id");

        Assert.Equal(3, rating);
    }

    [Fact]
    public async Task Returns_Zero_If_No_Reviews()
    {        
        var expectedReviews = new List<Review>() {};

        var ratingCalculator = new RatingCalculator(new Mock<IReviewDatabase>().Object);

        var rating = await ratingCalculator.GetRating("Some Album Id");

        Assert.Equal(0, rating);
    }
}