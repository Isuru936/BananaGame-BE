using BananaGame.Api.Extensions;
using BananaGame.Application;
using BananaGame.Identity;
using BananaGame.Persistance;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var cfg = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(); // This registers IHttpClientFactory
builder.Services.ConfigureApplicationService();
builder.Services.ConfigurePersistanceService(cfg);
builder.Services.ConfigureIdentityService(cfg);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.Services.RegisterSerilogLogging(cfg);

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a Valid Token NO NEED THE KEYWORD BEARER JUST PSATE THE TOKEN",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:5173") // Frontend URL
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


var app = builder.Build();

app.MapEndpoint();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");


app.Run();
