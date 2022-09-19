using System.Linq;
using System.Threading.Tasks;
using Models;
using Xunit;

public class StubAlbumDatabaseTest
{
    [Fact]
    public async Task Save_And_Retrieve_Works()
    {
        var album = new Album() { AlbumId = "Album Id 1" }; 

        await new StubAlbumDatabase().Save(album);

        var result = await new StubAlbumDatabase().Get("Album Id 1");

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Saving_AlbumId_Twice_Overwrites()
    {
        var album = new Album() { AlbumId = "Album Id 2", Name = "Album Name" };
        var overwrittenAlbum = new Album() { AlbumId = "Album Id 2", Name = "Overwritten Album Name" };

        await new StubAlbumDatabase().Save(album);

        await new StubAlbumDatabase().Save(overwrittenAlbum);

        var result = await new StubAlbumDatabase().Get("Album Id 2");

        Assert.Equal(overwrittenAlbum.Name, result.Name);
    }

    [Fact]
    public async Task Only_One_Album_Exists_For_Id()
    {    
        var album = new Album() { AlbumId = "Album Id 2", Name = "Album Name" };
        var overwrittenAlbum = new Album() { AlbumId = "Album Id 2", Name = "Overwritten Album Name" };

        await new StubAlbumDatabase().Save(album);

        await new StubAlbumDatabase().Save(overwrittenAlbum);

        var albums = await new StubAlbumDatabase().GetAll();

        albums.Single(x => x.AlbumId == "Album Id 2");
    }

    [Fact]
    public async Task Deletes_Album() 
    {
        var expectedAlbum = new Album() { AlbumId = "Album Id 3" };

        await new StubAlbumDatabase().Save(expectedAlbum);

        await new StubAlbumDatabase().Delete(expectedAlbum.AlbumId);

        var album = await new StubAlbumDatabase().Get(expectedAlbum.AlbumId);

        Assert.Null(album);
    }
}