using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class IMDbController: Controller
{
    public async Task<T?> ParseBody<T>()
    {
        var stream = new StreamReader(Request.Body);
        var body = await stream.ReadToEndAsync();
        
        var album = JsonConvert.DeserializeObject<T>(body);
        
        return album;
    }
}