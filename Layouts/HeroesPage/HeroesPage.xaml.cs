using MarvelGallery.Layouts.HeroesPage.ViewModels;

namespace MarvelGallery.Layouts.HeroesPage;

public partial class HeroesPage : ContentPage
{

    private HeroesPageViewModel _viewModel;
    public HeroesPage()
	{
		InitializeComponent();
        _viewModel = (HeroesPageViewModel)BindingContext;
    }

    private async void OnSearchButtonClicked(object sender, EventArgs e)
    {
        charactersCollectionView.IsEnabled = false;
        activityIndicator.IsRunning = true;
        string searchTerm = searchEntry.Text;
        await _viewModel.LoadCharacters(searchTerm).ContinueWith(
            result =>
            {
                activityIndicator.IsRunning = false;
            }, TaskScheduler.FromCurrentSynchronizationContext()
            );
    }
}