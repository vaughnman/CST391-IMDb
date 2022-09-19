using System.Threading.Tasks;
using Controllers;
using Databases;
using Moq;
using Xunit;

public class AlbumController_Delete_Test
{

    [Fact]
    public async Task Deletes_Album()
    {
        var albumDatabase = new Mock<IAlbumDatabase>();
        var reviewDatabase = new Mock<IReviewDatabase>();
        
        var albumController = new AlbumController(albumDatabase.Object, reviewDatabase.Object);

        await albumController.DeleteInternal("Some Id");

        albumDatabase.Verify(x=>x.Delete("Some Id"));        
    }

    [Fact]
    public async Task Deletes_Album_Reviews()
    {
        var albumDatabase = new Mock<IAlbumDatabase>();
        var reviewDatabase = new Mock<IReviewDatabase>();
        
        var albumController = new AlbumController(albumDatabase.Object, reviewDatabase.Object);

        await albumController.DeleteInternal("Some Album Id");

        reviewDatabase.Verify(x=>x.DeleteForAlbum("Some Album Id"));   
    }
}