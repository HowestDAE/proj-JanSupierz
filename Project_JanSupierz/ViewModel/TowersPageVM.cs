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

        public async void Load()
        {
            var pair = await Repository.LoadTowersAsync();

            CurrentTowers = pair.Item1;
            TowerTypes = pair.Item2;

            OnPropertyChanged(nameof(CurrentTowers));
            OnPropertyChanged(nameof(TowerTypes));
            
        }

        private async void LoadTowers(string value)
        {
            CurrentTowers = await Repository.GetTowersAsync(value);
            OnPropertyChanged(nameof(CurrentTowers));
        }
    }
}
