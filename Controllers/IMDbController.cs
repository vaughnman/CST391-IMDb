using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class IMDbController: Controller
{
    /// <summary>
    /// Parses request body to object from json
    /// </summary>
    /// <typeparam name="T">Request body type</typeparam>
    /// <returns>Object from request body</returns>
    public async Task<T?> ParseBody<T>()
    {
        var stream = new StreamReader(Request.Body);
        var body = await stream.ReadToEndAsync();
        
        return JsonConvert.DeserializeObject<T>(body);
    }
}