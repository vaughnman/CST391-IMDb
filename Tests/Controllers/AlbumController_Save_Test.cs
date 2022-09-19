using System;
using System.Threading.Tasks;
using Controllers;
using Databases;
using Models;
using Moq;
using Xunit;

public class AlbumController_Save_Test 
{
    [Fact]
    public async Task Saves_Album_With_Timestamp() 
    {        
        var database = new Mock<IAlbumDatabase>();
        
        var albumController = new AlbumController(database.Object, null);

        var expectedAlbum = new Album() { TimestampAddedMs = 1, AlbumId = "Some Album Id", Name = "Some Name" };

        await albumController.SaveInternal(expectedAlbum);

        database.Verify(x => x.Save(expectedAlbum));
    }

    [Fact]
    public async Task Adds_Timestamp_If_Nonexistent()
    {
        var database = new Mock<IAlbumDatabase>();
        
        var albumController = new AlbumController(database.Object, null);

        await albumController.SaveInternal(new Album() { Name = "Some Name" });

        var currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        database.Verify(x => x.Save(It.Is<Album>(x => currentTime - x.TimestampAddedMs < 1000)));
    }

    [Fact]
    public async Task Adds_AlbumId_If_Nonexistent()
    {
        var database = new Mock<IAlbumDatabase>();
        
        var albumController = new AlbumController(database.Object, null);

        await albumController.SaveInternal(new Album() { Name = "Some Name" });

        var currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        database.Verify(x => x.Save(It.Is<Album>(x => x.AlbumId != null)));
    }

    [Fact]
    public async Task Album_Name_Is_Required() 
    {
        var database = new Mock<IAlbumDatabase>();
        
        var albumController = new AlbumController(database.Object, null);

        await Assert.ThrowsAnyAsync<Exception>(
            async () => await albumController.SaveInternal(new Album() { Name = "" })
        );
        await Assert.ThrowsAnyAsync<Exception>(
            async () => await albumController.SaveInternal(new Album() { Name = null })
        );
    }
}