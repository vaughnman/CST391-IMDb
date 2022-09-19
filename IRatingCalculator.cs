public interface IRatingCalculator 
{
    public Task<double> GetRating(string albumId);
}