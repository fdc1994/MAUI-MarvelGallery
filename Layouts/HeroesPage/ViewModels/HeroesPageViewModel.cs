using MarvelGallery.DataModels;
using MarvelGallery.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MarvelGallery.Layouts.HeroesPage.ViewModels
{
    internal class HeroesPageViewModel : INotifyPropertyChanged
    {
        private readonly MarvelApiService _marvelApiService;
        private ObservableCollection<Character> _characters;

        public HeroesPageViewModel()
        {
            _marvelApiService = new MarvelApiService();
            Characters = new ObservableCollection<Character>();
        }

        public ObservableCollection<Character> Characters
        {
            get => _characters;
            set
            {
                _characters = value;
                    OnPropertyChanged();
            }
        }

        public async Task LoadCharacters(string searchTerm)
        {
                var result = await _marvelApiService.GetCharacters(searchTerm);
                Characters.Clear();
                if (result.code == 200 && result.data.results.Count > 0)
                {
                    foreach (var character in result.data.results)
                    {
                        String fullPath = character.thumbnail.path + "." + character.thumbnail.extension;
                        //Needed because Android does not support unsecure protocol image loading
                        string finalFullPath = fullPath.Replace("http", "https");
                        character.thumbnail.fullPath = finalFullPath;
                        Characters.Add(character);
                    }
                }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
