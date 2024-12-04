using BananaGame.Application.Features.Question.Response;

namespace BananaGame.Application.Abstractions.Services
{
    public interface IQuestionAPIClient
    {
        Task<QuestionResponse> FetchAQuestion();
    }
}
