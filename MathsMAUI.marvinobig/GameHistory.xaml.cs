namespace MathsMAUI;

public partial class GameHistory : ContentPage
{
    private string _page { get; set; }
    public string PageName
    {
        get { return $"{_page} Screen"; }
    }
    public GameHistory(string page)
    {
        InitializeComponent();
        _page = page;
        BindingContext = this;
        history.ItemsSource = App.GameRepository.GetAllHistory();
    }

    private void DeleteGameHistory(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int historyId = (int)btn.BindingContext;

        App.GameRepository.DeleteHistory(historyId);

        history.ItemsSource = App.GameRepository.GetAllHistory();
    }
}