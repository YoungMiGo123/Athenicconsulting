namespace AthenicConsulting.Seeder.Seeder.Models
{
    public interface IHttpService
    {
        Task<T> Get<T>(string Url, Dictionary<string, string> Headers = null);
        Task<T> Post<T>(string Url, string json, Dictionary<string, string> Headers = null);
        Task<T> PostWithBody<T>(string url, string jsonRequest);
    }
}