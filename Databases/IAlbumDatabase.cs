using Models;

namespace Databases;

public interface IAlbumDatabase
{
    public Task Save(Album album);
    public Task Delete(string albumId);

    public Task<IEnumerable<Album>> GetAll();
    public Task<Album> Get(string albumId);
} 