using Models;
using MySql.Data.MySqlClient;

/// <summary>
/// Database for reviews
/// </summary>
public class ReviewDatabase : MySqlDatabase, IReviewDatabase
{
    /// <summary>
    /// Constructor
    /// </summary>
    public ReviewDatabase(): base() {}

    /// <summary>
    /// Adds a review to the database
    /// </summary>
    public async Task Add(Review review) 
    {
        var connection = OpenConnection();
        await AddCommand(review, connection).ExecuteNonQueryAsync();
        connection.Close();
    }

    public MySqlCommand AddCommand(Review review, MySqlConnection connection)
    {        
        var command = new MySqlCommand(@"
            REPLACE reviews
                (`ReviewId`,`AlbumId`,`Username`,`Title`,`Body`,`Rating`,`TimestampAddedMs`)
            VALUES
                (@reviewId,@albumId,@username,@title,@body,@rating,@timestampAddedMs);",
            connection
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

    /// <summary>
    /// Deletes a review via its review id
    /// </summary>
    public async Task Delete(string reviewId) {
        var connection = OpenConnection();
        await DeleteCommand(reviewId, connection).ExecuteNonQueryAsync();
        connection.Close();
    }

    private MySqlCommand DeleteCommand(string reviewId, MySqlConnection connection)
    {
        var command = new MySqlCommand(@"DELETE FROM reviews WHERE ReviewId = @reviewId", connection);
        
        command.Parameters.AddWithValue("@reviewId", reviewId);

        return command;
    }

    /// <summary>
    /// Deletes all reviews for an albumId
    /// </summary>
    public async Task DeleteByAlbum(string albumId) {
        var connection = OpenConnection();
        await DeleteForAlbumCommand(albumId, connection).ExecuteNonQueryAsync();
        connection.Close();
    }

    private MySqlCommand DeleteForAlbumCommand(string albumId, MySqlConnection connection)
    {
        var command = new MySqlCommand(@"DELETE FROM reviews WHERE AlbumId = @albumId", connection);

        command.Parameters.AddWithValue("@albumId", albumId);
        
        return command;
    }

    /// <summary>
    /// Gets all reviews by albumId
    /// </summary>
    public async Task<IEnumerable<Review>> Get(string albumId) {
        var connection = OpenConnection();
        var reviews = await ReadAsList<Review>(GetCommand(albumId, connection));
        connection.Close();

        return reviews;
    }

    private MySqlCommand GetCommand(string albumId, MySqlConnection connection) 
    {
        var command = new MySqlCommand("SELECT * FROM reviews WHERE AlbumId = @albumId", connection);
        
        command.Parameters.AddWithValue("@albumId", albumId);
        
        return command;
    }
}