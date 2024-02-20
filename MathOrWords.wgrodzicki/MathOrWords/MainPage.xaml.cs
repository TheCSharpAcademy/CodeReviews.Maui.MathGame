using MathOrWords.Models;

namespace MathOrWords
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Window.MinimumHeight = 600;
            this.Window.MinimumWidth = 800;
        }


        /// <summary>
        /// Displays the math game mode variants menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMathGameChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MathVariantPage());
        }


        /// <summary>
        /// Handles initialization of the words game mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWordsGameChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WordsGamePage());
        }


        /// <summary>
        /// Displays previous game scores stored in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnScoresChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScoresPage());
        }


        /// <summary>
        /// Displays credits.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreditsChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreditsPage());
        }
    }
}
