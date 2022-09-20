using System.Linq;
using System.Threading.Tasks;
using Models;
using Xunit;


public class AlbumDatabaseTest
{
    [Fact]
    public async Task Save_And_Retrieve_Works()
    {
        var album = new Album() { AlbumId = "Album Id 1" }; 

        await new AlbumDatabase().Save(album);

        var result = await new AlbumDatabase().Get("Album Id 1");

        Assert.NotNull(result);

        await TryCleanupAlbum("Album Id 1");
    }

    [Fact]
    public async Task Saving_AlbumId_Twice_Overwrites()
    {
        var album = new Album() { AlbumId = "Album Id 2", Name = "Album Name" };
        var overwrittenAlbum = new Album() { AlbumId = "Album Id 2", Name = "Overwritten Album Name" };

        await new AlbumDatabase().Save(album);

        await new AlbumDatabase().Save(overwrittenAlbum);

        var result = await new AlbumDatabase().Get("Album Id 2");

        Assert.Equal(overwrittenAlbum.Name, result.Name);

        await TryCleanupAlbum("Album Id 2");
    }

    [Fact]
    public async Task Only_One_Album_Exists_For_Id()
    {    
        var album = new Album() { AlbumId = "Album Id 3", Name = "Album Name" };
        var overwrittenAlbum = new Album() { AlbumId = "Album Id 3", Name = "Overwritten Album Name" };

        await new AlbumDatabase().Save(album);

        await new AlbumDatabase().Save(overwrittenAlbum);

        var albums = await new AlbumDatabase().GetAll();

        albums.Single(x => x.AlbumId == "Album Id 3");

        await TryCleanupAlbum("Album Id 3");
    }

    [Fact]
    public async Task Deletes_Album() 
    {
        var expectedAlbum = new Album() { AlbumId = "Album Id 3" };

        await new AlbumDatabase().Save(expectedAlbum);

        await new AlbumDatabase().Delete(expectedAlbum.AlbumId);

        var album = await new AlbumDatabase().Get(expectedAlbum.AlbumId);

        Assert.Null(album);
    }

    public async Task TryCleanupAlbum(string albumId)
    {
        try {
            await new AlbumDatabase().Delete(albumId);
        } catch { }
    }
}