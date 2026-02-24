using FitnessQuest.Api.Models;

namespace FitnessQuest.Api.Services;

public static class QuestGenerator
{
    private static readonly Random Rng = new();

    private record QuestTemplate(string Title, string Template, QuestDifficulty Difficulty, string Type);

    private static readonly QuestTemplate[] Templates =
    [
        new("Walk Quest", "Walk for {minutes} minutes at an easy pace.", QuestDifficulty.Easy, "Cardio"),
        new("Squat Starter", "Do {sets} sets of {reps} squats. Rest {rest}s.", QuestDifficulty.Easy, "Strength"),

        new("Walk + Bursts", "Walk for {minutes} minutes. Add 3x 30s faster segments.", QuestDifficulty.Medium, "Cardio"),
        new("Core Builder", "Do {rounds} rounds: plank {plank}s + {reps} crunches.", QuestDifficulty.Medium, "Core"),

        new("Intervals", "Walk for {minutes} minutes: alternate 1 min fast / 2 min easy.", QuestDifficulty.Hard, "Cardio"),
        new("Strength Circuit", "Do {rounds} rounds: {reps} squats + {reps2} push-ups + plank {plank}s.", QuestDifficulty.Hard, "Strength"),
    ];

    public static QuestDto Generate(QuestDifficulty difficulty)
    {
        var pool = Templates.Where(t => t.Difficulty == difficulty).ToArray();
        var chosen = pool[Rng.Next(pool.Length)];

        var ranges = difficulty switch
        {
            QuestDifficulty.Easy   => new Ranges(Sets:(2,3), Reps:(8,12), Rest:(45,75), Minutes:(10,20), Rounds:(2,3), Plank:(15,30), Xp:50),
            QuestDifficulty.Medium => new Ranges(Sets:(3,4), Reps:(10,15), Rest:(30,60), Minutes:(20,30), Rounds:(3,4), Plank:(30,60), Xp:100),
            _                     => new Ranges(Sets:(4,5), Reps:(12,20), Rest:(20,45), Minutes:(30,45), Rounds:(4,6), Plank:(60,120), Xp:175),
        };

        int sets = Next(ranges.Sets);
        int reps = Next(ranges.Reps);
        int rest = Next(ranges.Rest);
        int minutes = Next(ranges.Minutes);
        int rounds = Next(ranges.Rounds);
        int plank = Next(ranges.Plank);

        int reps2 = Math.Max(8, reps - 2);

        string desc = chosen.Template
            .Replace("{sets}", sets.ToString())
            .Replace("{reps}", reps.ToString())
            .Replace("{reps2}", reps2.ToString())
            .Replace("{rest}", rest.ToString())
            .Replace("{minutes}", minutes.ToString())
            .Replace("{rounds}", rounds.ToString())
            .Replace("{plank}", plank.ToString());

        return new QuestDto(chosen.Title, desc, difficulty.ToString(), ranges.Xp, chosen.Type);
    }

    private static int Next((int min, int max) r) => Rng.Next(r.min, r.max + 1);

    private record Ranges(
        (int min, int max) Sets,
        (int min, int max) Reps,
        (int min, int max) Rest,
        (int min, int max) Minutes,
        (int min, int max) Rounds,
        (int min, int max) Plank,
        int Xp
    );
}