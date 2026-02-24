using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json.Serialization;

using FitnessQuest.Api.Models;
using FitnessQuest.Api.Services;

var builder = WebApplication.CreateSlimBuilder(args);

// Keep the slim-template JSON source-gen chain
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// OpenAPI / Swagger (slim template style)
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ----------------------------
// Quests (Minimal API endpoints)
// ----------------------------

var questsApi = app.MapGroup("/quests");

// POST /quests/generate
questsApi.MapPost(
    "/generate",
    Results<Ok<QuestDto>, BadRequest<ErrorDto>> (GenerateQuestRequest req) =>
    {
        if (!Enum.TryParse<QuestDifficulty>(req.Difficulty, ignoreCase: true, out var difficulty))
            return TypedResults.BadRequest(new ErrorDto("Invalid difficulty. Use Easy, Medium, or Hard."));

        var quest = QuestGenerator.Generate(difficulty);
        return TypedResults.Ok(quest);
    }
)
.WithName("GenerateQuest");

app.Run();

// ----------------------------
// JSON source-gen registrations
// IMPORTANT: keep in sync with your Models
// ----------------------------

[JsonSerializable(typeof(QuestDto))]
[JsonSerializable(typeof(GenerateQuestRequest))]
[JsonSerializable(typeof(ErrorDto))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}