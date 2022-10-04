using Databases;
using Models;
using MySql.Data.MySqlClient;

/// <summary>
/// Database for albums
/// </summary>
public class AlbumDatabase : MySqlDatabase, IAlbumDatabase
{
    /// <summary>
    /// Constructor
    /// </summary>
    public AlbumDatabase(): base() { }

    /// <summary>
    /// Deletes album by id
    /// </summary>
    public async Task Delete(string albumId) 
    {
        var connection = OpenConnection();
        await DeleteCommand(albumId, connection).ExecuteNonQueryAsync();
        connection.Close();
    }

    private MySqlCommand DeleteCommand(string albumId, MySqlConnection connection)
    {
        var command = new MySqlCommand(@"DELETE FROM albums WHERE AlbumId = @albumId", connection);
     
        command.Parameters.AddWithValue("@albumId", albumId);

        return command;
    }

    /// <summary>
    /// Creates or updates an album
    /// </summary>
    public async Task Save(Album album) 
    {
        var connection = OpenConnection();
        await SaveCommand(album, connection).ExecuteNonQueryAsync();
        connection.Close();
    }

    private MySqlCommand SaveCommand(Album album, MySqlConnection connection)
    {
        var command = new MySqlCommand(@"
            REPLACE albums
                (`AlbumId`,`Name`,`Band`,`ImageUrl`,`ReleaseDate`,`TimestampAddedMs`)
            VALUES
                (@albumId,@name,@band,@imageUrl,@releaseDate,@timestampAddedMs);",
            connection
        );

        command.Parameters.AddWithValue("@albumId", album.AlbumId);
        command.Parameters.AddWithValue("@name", album.Name);
        command.Parameters.AddWithValue("@band", album.Band);
        command.Parameters.AddWithValue("@imageUrl", album.ImageUrl);
        command.Parameters.AddWithValue("@releaseDate", album.ReleaseDate);
        command.Parameters.AddWithValue("@timestampAddedMs", album.TimestampAddedMs);
        
        return command;
    }

    /// <summary>
    /// Gets an album by id
    /// </summary>
    public async Task<Album> Get(string albumId)
    {
        var connection = OpenConnection();
        var albums = await ReadAsList<Album>(GetCommand(albumId, connection));
        connection.Close();
        
        if (albums.Count() == 0) return null;
        else return albums.First();
    }

    private MySqlCommand GetCommand(string albumId, MySqlConnection connection)
    {
        var command = new MySqlCommand("SELECT * FROM albums where AlbumId = @albumId", connection);
        
        command.Parameters.AddWithValue("@albumId", albumId);
        
        return command;
    }

    /// <summary>
    /// Get all albums
    /// </summary>
    public async Task<IEnumerable<Album>> GetAll() 
    {
        var connection = OpenConnection();
        var result = GetAllCommand(connection);
        
        var albums = await ReadAsList<Album>(result);
        connection.Close();
        
        return albums;
    }

    private MySqlCommand GetAllCommand(MySqlConnection connection) =>
        new MySqlCommand("SELECT * FROM albums", connection);
}