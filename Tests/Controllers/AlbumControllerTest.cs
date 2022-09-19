using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using Databases;
using Models;
using Moq;
using Xunit;

public class AlbumControllerTest 
{
    [Fact]
    public async Task Gets_Album_From_Database()
    {
        var database = new Mock<IAlbumDatabase>();
        var expectedAlbum = new Album();

        database.Setup(x=>x.Get("Some Id")).ReturnsAsync(expectedAlbum);
        
        var albumController = new AlbumController(database.Object, null);

        var resultAlbum = await albumController.GetInternal("Some Id");

        Assert.Equal(resultAlbum, expectedAlbum);
    }

    [Fact]
    public async Task Gets_All_Albums_From_Database() 
    {
        var database = new Mock<IAlbumDatabase>();
        var expectedAlbums = new List<Album>() { new Album(), new Album() };

        database.Setup(x=>x.GetAll()).ReturnsAsync(expectedAlbums);
        
        var albumController = new AlbumController(database.Object, null);

        var resultAlbums = await albumController.GetAllInternal();

        Assert.Equal(resultAlbums, expectedAlbums);
    }
}