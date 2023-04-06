using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project_JanSupierz.Model;
using Project_JanSupierz.Repository;
using Project_JanSupierz.View.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.ViewModel
{
    public class TowersPageVM : ObservableObject
    {
        public IBloonsTDRepository Repository { get; set; }
        public RelayCommand ChangeRepositoryCommand { get; private set; }
        public string RepositoryButtonText { get; set; }

        public List<Tower> CurrentTowers { get; set; }
        public List<string> TowerTypes { get; set; }

        private string _selectedType = "All Types";
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                LoadTowers(value);
                _selectedType = value;
            }
        }

        public Tower SelectedTower { get; set; }

        public async void Load(RelayCommand changeRepositoryCommand)
        {
            await Task.Run(() => Load());

            ChangeRepositoryCommand = changeRepositoryCommand;
            OnPropertyChanged(nameof(ChangeRepositoryCommand));
        }

        //This function is used to load all data from a new repository
        public async void Load()
        {
            RepositoryButtonText = "Loading...";
            OnPropertyChanged(nameof(RepositoryButtonText));

            var pair = await Repository.LoadTowersAsync();

            CurrentTowers = pair.Item1;
            TowerTypes = pair.Item2;

            OnPropertyChanged(nameof(CurrentTowers));
            OnPropertyChanged(nameof(TowerTypes));

            //Selected type is not "All types"
            if (_selectedType != TowerTypes.Last())
            {
                LoadTowers(_selectedType);
            }

            UpdateRepositoryButtonText();
        }

        //This function loads selected towers
        private async void LoadTowers(string value)
        {
            CurrentTowers = await Repository.GetTowersAsync(value);
            OnPropertyChanged(nameof(CurrentTowers));
        }

        private void UpdateRepositoryButtonText()
        {
            if (Repository.GetType() == typeof(BloonsTDApiRepository))
            {
                RepositoryButtonText = "Use Local Repository";
            }
            else
            {
                RepositoryButtonText = "Use API Repository";
            }

            OnPropertyChanged(nameof(RepositoryButtonText));
        }
    }
}
