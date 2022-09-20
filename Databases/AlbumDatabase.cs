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
    public Task Delete(string albumId) =>
        DeleteCommand(albumId).ExecuteNonQueryAsync();

    private MySqlCommand DeleteCommand(string albumId)
    {
        var command = new MySqlCommand(@"DELETE FROM albums WHERE AlbumId = @albumId", _connection);
     
        command.Parameters.AddWithValue("@albumId", albumId);

        return command;
    }

    /// <summary>
    /// Creates or updates an album
    /// </summary>
    public Task Save(Album album) =>
        SaveCommand(album).ExecuteNonQueryAsync();

    private MySqlCommand SaveCommand(Album album)
    {
        var command = new MySqlCommand(@"
            REPLACE albums
                (`AlbumId`,`Name`,`Band`,`ImageUrl`,`ReleaseDate`,`TimestampAddedMs`)
            VALUES
                (@albumId,@name,@band,@imageUrl,@releaseDate,@timestampAddedMs);",
            _connection
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
        var albums = await ReadAsList<Album>(GetCommand(albumId));

        if (albums.Count() == 0) return null;
        else return albums.First();
    }

    private MySqlCommand GetCommand(string albumId)
    {
        var command = new MySqlCommand("SELECT * FROM albums where AlbumId = @albumId", _connection);
        
        command.Parameters.AddWithValue("@albumId", albumId);
        
        return command;
    }

    /// <summary>
    /// Get all albums
    /// </summary>
    public async Task<IEnumerable<Album>> GetAll() =>
        await ReadAsList<Album>(GetAllCommand());

    private MySqlCommand GetAllCommand() =>
        new MySqlCommand("SELECT * FROM albums", _connection);
}