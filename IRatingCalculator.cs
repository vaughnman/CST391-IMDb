/// <summary>
/// Rating Calculator interface
/// </summary>
public interface IRatingCalculator 
{
    /// <summary>
    /// Gets an album's rating
    /// </summary>
    public Task<double> GetRating(string albumId);
}