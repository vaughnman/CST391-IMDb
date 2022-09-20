using Models;
using MySql.Data.MySqlClient;

public class ReviewDatabase : MySqlDatabase, IReviewDatabase
{
    public ReviewDatabase(): base() {}

    public Task Add(Review review) =>
        AddCommand(review).ExecuteNonQueryAsync();

    public MySqlCommand AddCommand(Review review)
    {        
        var command = new MySqlCommand(@"
            REPLACE reviews
                (`ReviewId`,`AlbumId`,`Username`,`Title`,`Body`,`Rating`,`TimestampAddedMs`)
            VALUES
                (@reviewId,@albumId,@username,@title,@body,@rating,@timestampAddedMs);",
            _connection
        );

        command.Parameters.AddWithValue("@reviewId", review.ReviewId);
        command.Parameters.AddWithValue("@albumId", review.AlbumId);
        command.Parameters.AddWithValue("@username", review.Username);
        command.Parameters.AddWithValue("@title", review.Title);
        command.Parameters.AddWithValue("@body", review.Body);
        command.Parameters.AddWithValue("@rating", review.Rating);
        command.Parameters.AddWithValue("@timestampAddedMs", review.TimestampAddedMs);
        
        return command;
    }

    public Task Delete(string reviewId) =>
        DeleteCommand(reviewId).ExecuteNonQueryAsync();

    private MySqlCommand DeleteCommand(string reviewId)
    {
        var command = new MySqlCommand(@"DELETE FROM reviews WHERE ReviewId = @reviewId", _connection);
        
        command.Parameters.AddWithValue("@reviewId", reviewId);

        return command;
    }

    public Task DeleteByAlbum(string albumId)=>
        DeleteForAlbumCommand(albumId).ExecuteNonQueryAsync();

    private MySqlCommand DeleteForAlbumCommand(string albumId)
    {
        var command = new MySqlCommand(@"DELETE FROM reviews WHERE AlbumId = @albumId", _connection);

        command.Parameters.AddWithValue("@albumId", albumId);
        
        return command;
    }


    public async Task<IEnumerable<Review>> Get(string albumId)=>
        await ReadAsList<Review>(GetCommand(albumId));

    private MySqlCommand GetCommand(string albumId) 
    {
        var command = new MySqlCommand("SELECT * FROM reviews WHERE AlbumId = @albumId", _connection);
        
        command.Parameters.AddWithValue("@albumId", albumId);
        
        return command;
    }
}