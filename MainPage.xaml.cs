namespace FitnessQuest.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(QuestViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}