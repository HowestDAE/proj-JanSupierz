using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project_JanSupierz.Model;
using Project_JanSupierz.Repository;
using Project_JanSupierz.View.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project_JanSupierz.ViewModel
{
    public class TowerPageVM: ObservableObject
    {
        //Variables from main window
        public IBloonsTDRepository Repository { get; set; }
        public string Id { get; set; }

        private Tower _currentTower = new Tower { };
        public Tower CurrentTower
        {
            get
            {
                return _currentTower;
            }
            set
            {
                if (value == null) return;

                _currentTower = value;
                _currentTower.Description = TextToLines(_currentTower.Description);

                //Load first path
                _currentPathIndex = 0;
                LoadCurrentPath();

                OnPropertyChanged(nameof(CurrentTower));
            }
        }

        //Upgrade paths
        private int _currentPathIndex;
        private List<Upgrade> _currentPath = new List<Upgrade>();
        public List<Upgrade> CurrentPath { get { return _currentPath; } set { _currentPath = value; OnPropertyChanged(nameof(CurrentPath)); } }

        private Upgrade _selectedUpgrade = null;
        public Upgrade SelectedUpgrade
        {
            get { return _selectedUpgrade; }
            set
            {
                _selectedUpgrade = value;

                CopyUpgradeValues();
            }
        }

        //Reset button
        public string CommandText { get; set; } = "";

        //Load info
        private void LoadCurrentPath()
        {
            if (CurrentTower == null) return;

            //Check if range is correct
            if (CurrentTower.Paths.Count > _currentPathIndex && _currentPathIndex >= 0) 
            {
                CurrentPath = CurrentTower.Paths[_currentPathIndex];
            }
        }

        private async void ResetTower()
        {
            _currentTower = (Tower)(await Repository.GetTowerAsync(Id)).Clone();
            _currentTower.Description = TextToLines(_currentTower.Description);
            OnPropertyChanged(nameof(CurrentTower));

            CommandText = "";
            OnPropertyChanged(nameof(CommandText));
        }

        //Commands
        public RelayCommand PreviousUpgradesCommand { get; private set; }
        public RelayCommand NextUpgradesCommand { get; private set; }
        public RelayCommand ResetTowerCommand { get; private set; }

        private void NextUpgrades()
        {
            if (CurrentTower == null) return;

            if (_currentPathIndex >= CurrentTower.Paths.Count - 1) 
            {
                _currentPathIndex = 0;
            }
            else
            {
                ++_currentPathIndex;
            }

            LoadCurrentPath();
        }

        private void PreviousUpgrades()
        {
            if (CurrentTower == null) return;

            if (_currentPathIndex <= 0)
            {
                _currentPathIndex = CurrentTower.Paths.Count - 1;
            }
            else
            {
                --_currentPathIndex;
            }

            LoadCurrentPath();
        }

        //Other
        void CopyUpgradeValues()
        {
            if (_selectedUpgrade == null) return;

            _currentTower.Description = TextToLines(_selectedUpgrade.Description);

            _currentTower.Cost = _selectedUpgrade.Cost;
            _currentTower.Name = _selectedUpgrade.Name;
            _currentTower.Id = _selectedUpgrade.Id;

            _currentTower.Stats.AttackSpeed = "-";
            _currentTower.Stats.Damage = "-";
            _currentTower.Stats.Pierce = "-";
            _currentTower.Stats.Range = "-";

            CommandText = "Reset";
            OnPropertyChanged(nameof(CommandText));

            OnPropertyChanged(nameof(CurrentTower));
        }

        public TowerPageVM()
        {
            ResetTowerCommand = new RelayCommand(ResetTower);
            PreviousUpgradesCommand = new RelayCommand(PreviousUpgrades);
            NextUpgradesCommand = new RelayCommand(NextUpgrades);
        }

        private string TextToLines(string text, int nrCharactersPerLine = 60)
        {
            int nrCharacters = 0;

            for (int i = 0; i < text.Length; i++)
            {
                nrCharacters++;
                if (nrCharacters >= nrCharactersPerLine && text[i] == ' ')
                {
                    text = text.Remove(i, 1).Insert(i, "\n");
                    nrCharacters = 0;
                }
            }

            return text;
        }
    }
}
