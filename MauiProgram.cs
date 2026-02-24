using Microsoft.Extensions.Logging;

namespace FitnessQuest.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // -----------------------------
            // 🔥 ADD THIS SECTION
            // -----------------------------

            builder.Services.AddSingleton<HttpClient>(sp =>
            {
#if ANDROID
    return new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5243/") };
#else
                return new HttpClient { BaseAddress = new Uri("http://localhost:5243/") };
#endif
            });

            builder.Services.AddSingleton<QuestApiClient>();
            builder.Services.AddSingleton<QuestViewModel>();

            // -----------------------------

            return builder.Build();
        }
    }
}