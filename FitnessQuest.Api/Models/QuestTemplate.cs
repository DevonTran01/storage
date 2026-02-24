using FitnessQuest.Api.Models;

public class QuestTemplate
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = "";
    public string DescriptionTemplate { get; set; } = ""; // e.g. "Do {sets}x{reps} squats"
    public QuestDifficulty Difficulty { get; set; }
    public string Type { get; set; } = ""; // Cardio/Strength/etc
}