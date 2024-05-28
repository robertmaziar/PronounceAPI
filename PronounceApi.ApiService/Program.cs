using PronounceApi.ApiService.Models;
using PronounceApi.ApiService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("PronounceAPIDatabase"));

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<WordsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/api/v1/words", async (WordsService wordsService) =>
{
    var words = await wordsService.GetAsync();
    return Results.Ok(words);
});

app.MapGet("/api/v1/words/{baseWord}", async (WordsService wordsService, string baseWord) =>
{
    var word = await wordsService.GetAsync(baseWord);
    if (word != null)
    {
        return Results.Ok(word);
    }
    else
    {
        return Results.NotFound($"Word with base word '{baseWord}' not found.");
    }
});


app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
