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
        string searchTerm = searchEntry.Text;
        await _viewModel.LoadCharacters(searchTerm);
    }
}