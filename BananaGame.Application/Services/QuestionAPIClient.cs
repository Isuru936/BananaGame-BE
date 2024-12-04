using BananaGame.Application.Abstractions.Services;
using BananaGame.Application.Features.Question.Response;
using System.Text.Json;

namespace BananaGame.Application.Services
{
    public class QuestionAPIClient : IQuestionAPIClient
    {
        protected readonly IHttpClientFactory _httpClientFactory;

        public QuestionAPIClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<QuestionResponse> FetchAQuestion()
        {
            var endpointUrl = new Uri($"https://marcconrad.com/uob/banana/api.php", UriKind.Absolute);

            try
            {
                using (var client = _httpClientFactory.CreateClient())
                {
                    var response = await client.GetAsync(endpointUrl);
                    response.EnsureSuccessStatusCode();

                    using (var content = await response.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<QuestionResponse>(content);
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}
