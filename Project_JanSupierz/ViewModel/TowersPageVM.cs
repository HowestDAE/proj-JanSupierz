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
        private BloonsTDApiRepository _bloomTDApiRepository = new BloonsTDApiRepository();
        private BloonsTDLocalRepository _bloonsTDLocalRepository = new BloonsTDLocalRepository();
        private IBloonsTDRepository _bloonsTDRepository = null;

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

        private async void LoadTowers()
        {
            CurrentTowers = await _bloonsTDRepository.GetTowersAsync();
            OnPropertyChanged(nameof(CurrentTowers));

            TowerTypes = await _bloomTDApiRepository.GetTowerTypesAsync();
            OnPropertyChanged(nameof(TowerTypes));
        }

        private async void LoadTowers(string value)
        {
            CurrentTowers = await _bloonsTDRepository.GetTowersAsync(value);
            OnPropertyChanged(nameof(CurrentTowers));
        }

        public TowersPageVM()
        {
            _bloonsTDRepository = _bloonsTDLocalRepository;
            BloonsConverter.UseApi = (_bloonsTDRepository == _bloomTDApiRepository);

            LoadTowers();
        }
    }
}
