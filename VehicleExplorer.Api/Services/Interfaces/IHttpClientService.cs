namespace VehicleExplorer.Api.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> GetResultsAsync<T>(string requestUri, CancellationToken cancellationToken);
    }
}
