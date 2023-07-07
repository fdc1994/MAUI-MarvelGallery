using MarvelGallery.Layouts.HeroesPage.ViewModels;

namespace MarvelGallery.Layouts.HeroesPage;

public partial class HeroesPage : ContentPage
{

    private HeroesPageViewModel _viewModel;
    public HeroesPage()
	{
		InitializeComponent();
        _viewModel = (HeroesPageViewModel)BindingContext;
        setupInitialView();
    }

    private async void setupInitialView()
    {
        await startSearch("");
    }



    private async void OnSearchButtonClicked(object sender, EventArgs e)
    {
        await startSearch(null);
    }

    private async Task startSearch(String customSearch)
    {
        charactersCollectionView.IsEnabled = false;
        activityIndicator.IsRunning = true;
        string searchTerm = searchEntry.Text;
        if (customSearch != null)
        {
            //Override search despite what's on the search bar
           searchTerm = customSearch;
        }
        
        await _viewModel.LoadCharacters(searchTerm).ContinueWith(
            result =>
            {
                activityIndicator.IsRunning = false;
            }, TaskScheduler.FromCurrentSynchronizationContext()
            );
    }
}