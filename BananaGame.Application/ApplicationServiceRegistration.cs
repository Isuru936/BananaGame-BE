using BananaGame.Application.Abstractions.Services;
using BananaGame.Application.Behaviours;
using BananaGame.Application.Features.Auth.Command;
using BananaGame.Application.Features.Auth.Validator;
using BananaGame.Application.Features.GameSession.Commands;
using BananaGame.Application.Features.GameSession.Validator;
using BananaGame.Application.Features.Player.Commands;
using BananaGame.Application.Features.Player.Validators;
using BananaGame.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BananaGame.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped<IValidator<CreatePlayerCommand>, CreatePlayerCommandValidator>();
            services.AddScoped<IValidator<UpdatePlayerCommand>, UpdatePlayerCommandValidator>();
            services.AddScoped<IValidator<StartGameSessionCommand>, CreateGameSessionCommandVaidator>();
            services.AddScoped<IValidator<SignUpCommand>, SignUpCommandValidator>();
            services.AddScoped<IValidator<SignInCommand>, SignInCommandValidator>();

            /*             services.AddHttpClient<IQuestionAPIClient, QuestionAPIClient>(client =>
                        {
                            client.BaseAddress = new Uri("https://marcconrad.com/uob/banana/api.php");
                            client.DefaultRequestHeaders.Add("Accept", "application/json");

                            // optional settings
                            client.Timeout = TimeSpan.FromSeconds(30);
                            client.DefaultRequestVersion = new Version(1, 0);
                            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;
                            client.MaxResponseContentBufferSize = 1_000_000;
                        });
            */

            services.AddScoped<IQuestionAPIClient, QuestionAPIClient>();



            return services;
        }


    }
}
