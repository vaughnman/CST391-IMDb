using Models;

namespace Databases;

/// <summary>
/// Album Database interface
/// </summary>
public interface IAlbumDatabase
{
    /// <summary>
    /// Creates or updates album
    /// </summary>
    public Task Save(Album album);
    /// <summary>
    /// Deletes album by id
    /// </summary>
    public Task Delete(string albumId);

    /// <summary>
    /// Get all albums
    /// </summary>
    public Task<IEnumerable<Album>> GetAll();
    /// <summary>
    /// Get album by id
    /// </summary>
    public Task<Album> Get(string albumId);
} 