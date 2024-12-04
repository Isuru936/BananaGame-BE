using System.Text.Json.Serialization;

namespace BananaGame.Application.Features.Question.Response
{
    public class QuestionResponse
    {
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;
        [JsonPropertyName("solution")]
        public int Solution { get; set; }
    }
}
