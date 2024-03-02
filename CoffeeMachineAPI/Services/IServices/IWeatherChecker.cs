namespace CoffeeMachineAPI.Services.IServices
{
    public interface IWeatherChecker
    {
        dynamic ApiConfig { get; }
        string ApiUrl { get; }
        Task<float?> GetTempAsync();
    }
}
