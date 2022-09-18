using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AthenicConsulting.Seeder.Seeder.Models
{
    public class CloudFlareImageRepo
    {
    }
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpService> _logger;

        public HttpService(ILogger<HttpService> logger)
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
            _logger = logger;
        }


        public async Task<T> PostWithBody<T>(string url, string jsonRequest)
        {
            try
            {
                var webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                byte[] byteResponse = webClient.UploadData(url, "POST", Encoding.ASCII.GetBytes(jsonRequest));
                string stringResult = Encoding.ASCII.GetString(byteResponse);
                var result = JsonConvert.DeserializeObject<T>(stringResult);
                return await Task.Run(() => result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while getting the response object. Exception = {ex}");
                return await Task.Run(() => default(T));
            }
        }
        public async Task<T> Post<T>(string Url, string json, Dictionary<string, string> Headers = null)
        {
            try
            {

                var request = new HttpRequestMessage();
                if (Headers != null)
                {
                    request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(Url),
                        Content = new StringContent(json, Encoding.UTF8, "application/json"),
                        Headers =
                        {
                                 { HttpRequestHeader.Authorization.ToString(), Headers.FirstOrDefault().Value },
                                 { HttpRequestHeader.Accept.ToString(), "application/json" },
                        }
                    };
                }
                else
                {
                    request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(Url),
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                }
                var response = await _httpClient.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"An error occured while processing this - {e}");
                return default;
            }
        }

        public async Task<T> Get<T>(string Url, Dictionary<string, string> Headers = null)
        {
            try
            {
                if (Headers != null)
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(Url),
                        Headers =
                        {
                            { HttpRequestHeader.Authorization.ToString(), Headers.FirstOrDefault().Value },
                            { HttpRequestHeader.Accept.ToString(), "application/json" },
                        }
                    };

                    var responseMessage = await _httpClient.SendAsync(httpRequestMessage);
                    if (responseMessage != null && responseMessage.IsSuccessStatusCode)
                    {
                        var response = await responseMessage.Content.ReadAsStringAsync();
                        var resultWithHeaders = JsonConvert.DeserializeObject<T>(response);
                        return resultWithHeaders;
                    }

                }

                var responseString = await _httpClient.GetStringAsync(Url);
                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"An error occured while processing this - {e}");
                return default;
            }
        }
    }
}
