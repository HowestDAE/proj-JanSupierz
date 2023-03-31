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

        private List<Tower> _currentTowers = new List<Tower>();
        public List<Tower> CurrentTowers { get { return _currentTowers; } set { _currentTowers = value; OnPropertyChanged(nameof(CurrentTowers)); } }

        private async void LoadTowers()
        {
            CurrentTowers = await _bloonsTDRepository.GetTowersAsync();
        }

        public TowersPageVM()
        {
            _bloonsTDRepository = _bloonsTDLocalRepository;
            BloonsConverter.UseApi = (_bloonsTDRepository == _bloomTDApiRepository);

            LoadTowers();
        }
    }
}
