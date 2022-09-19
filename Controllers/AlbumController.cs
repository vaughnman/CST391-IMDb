using System.Net;
using Databases;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace Controllers;

public class AlbumController : IMDbController
{
    private readonly IAlbumDatabase _albumDatabase;
    private readonly IReviewDatabase _reviewDatabase;

    public AlbumController(IAlbumDatabase albumDatabase, IReviewDatabase reviewDatabase)
    {
        _albumDatabase = albumDatabase;
        _reviewDatabase = reviewDatabase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var albumId = Request.Query["albumId"];

        var album = await GetInternal(albumId);

        return new OkObjectResult(album);
    }

    public async Task<Album> GetInternal(string albumId)
    {
        return await _albumDatabase.Get(albumId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var albums = await GetAllInternal();

        return new OkObjectResult(albums);
    }

    public async Task<IEnumerable<Album>> GetAllInternal()
    {
        return await _albumDatabase.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Save()
    {
        var album = await ParseBody<Album>();

        var savedAlbum = await SaveInternal(album);
        
        return CreatedAtAction("Saved Album", savedAlbum.AlbumId);
    }

    public async Task<Album> SaveInternal(Album album)
    {
        if(album.Name == null || album.Name == "")
            throw new ArgumentException("Album name is required!");

        if(album.TimestampAddedMs == 0) 
            album.TimestampAddedMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        if(album.AlbumId == null)
            album.AlbumId = Guid.NewGuid().ToString();

        await _albumDatabase.Save(album);
        
        return album;
    }

    [HttpDelete]    
    public async Task<IActionResult> Delete()
    {
        var albumId = Request.Query["albumId"];

        await DeleteInternal(albumId);

        return Accepted();
    }

    public async Task DeleteInternal(string albumId)
    {
        await _albumDatabase.Delete(albumId);
        await _reviewDatabase.DeleteForAlbum(albumId);
    }
}
