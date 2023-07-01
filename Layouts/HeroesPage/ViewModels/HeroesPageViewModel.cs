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
                if(value != null)
                {
                    OnPropertyChanged();
                }
            }
        }

        public async Task LoadCharacters(string searchTerm)
        {
            Characters.Clear();
            if (searchTerm != null)
            {
                var result = await _marvelApiService.GetCharacters(searchTerm);
                
                if (result.data.results.Count > 0)
                {
                    foreach (var character in result.data.results)
                    {
                        String fullPath = character.thumbnail.path + "." + character.thumbnail.extension;
                        string finalFullPath = fullPath.Replace("http", "https");
                        character.thumbnail.fullPath = finalFullPath;
                        Characters.Add(character);
                    }
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
