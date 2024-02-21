namespace MathOrWords;

public partial class MathVariantPage : ContentPage
{
	public MathVariantPage()
	{
		InitializeComponent();
		BindingContext = this;
	}


    /// <summary>
    /// Handles initialization of the math game mode.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMathOptionChosen(object sender, EventArgs e)
	{
		Button button = (Button)sender;

        Navigation.PushAsync(new MathGamePage(button.Text));
	}
}