using System.Collections.Concurrent;
using Databases;
using Models;

public class StubAlbumDatabase : IAlbumDatabase
{
    private static ConcurrentDictionary<String, Album> albums = new ConcurrentDictionary<String, Album>();

    public async Task Delete(string albumId)
    {
        albums.Remove(albumId, out var _);
    }

    public async Task<Album> Get(string albumId)
    {
        var matchingAlbums = albums.Where(x=> x.Key == albumId);

        if(matchingAlbums.Count() == 0) return null;
        
        return matchingAlbums.First().Value;
    }

    public async Task<IEnumerable<Album>> GetAll()
    {
        return albums.Values.ToList();
    }

    public async Task Save(Album album)
    {
        albums.AddOrUpdate(album.AlbumId, (key) => album, (key, old) => album);
    }
}