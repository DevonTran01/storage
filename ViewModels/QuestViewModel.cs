using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class QuestViewModel : INotifyPropertyChanged
{
    private readonly QuestApiClient _api;

    public QuestViewModel(QuestApiClient api)
    {
        _api = api;

        // Default selected difficulty
        SelectedDifficulty = "Easy";

        GenerateCommand = new Command(async () => await Generate());
    }

    // 🔥 ADD THIS RIGHT HERE
    public List<string> Difficulties { get; } =
        new() { "Easy", "Medium", "Hard" };

    public string SelectedDifficulty { get; set; }

    // ----------------------------

    private string _title = "";
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    private string _description = "";
    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); }
    }

    private string _reward = "";
    public string Reward
    {
        get => _reward;
        set { _reward = value; OnPropertyChanged(); }
    }

    public ICommand GenerateCommand { get; }

    private async Task Generate()
    {
        var quest = await _api.GenerateQuestAsync(SelectedDifficulty);
        if (quest == null) return;

        Title = quest.Title;
        Description = quest.Description;
        Reward = $"XP: {quest.XpReward}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}