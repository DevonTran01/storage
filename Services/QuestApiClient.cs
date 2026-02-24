using System.Net.Http.Json;

public class QuestApiClient
{
    private readonly HttpClient _http;

    public QuestApiClient(HttpClient http) => _http = http;

    public async Task<QuestDto?> GenerateQuestAsync(string difficulty)
    {
        var resp = await _http.PostAsJsonAsync("quests/generate", new { difficulty });
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<QuestDto>();
    }
}

public class QuestDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Difficulty { get; set; } = "";
    public int XpReward { get; set; }
}