using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project_JanSupierz.Model;
using Project_JanSupierz.Repository;
using Project_JanSupierz.View.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project_JanSupierz.ViewModel
{
    public class TowerPageVM: ObservableObject
    {
        private IBloonsTDRepository _bloomTDRepository = new BloonsTDApiRepository();

        private int _currentPathIndex;
        private List<UpgradePath> _currentPath = new List<UpgradePath>();

        private Tower _currentTower = new Tower
        {
            Cost = new Cost { Easy = 0, Medium = 0, Hard = 0, Impoppable = 0 },
            Name = "(name)",
            Description = "(description)",
            Stats = new Statistics { AttackSpeed = "0", Damage = "0", Pierce = "0", Range = "0", Type = "(type)" },
            Type = "(type)",
            Footprint = 0,
            DefaultHotkey = "(hotkey)",
            Id = "(id)"
        };

        //XAML - Bindings
        public Tower CurrentTower { get { return _currentTower; } set { _currentTower = value;  OnPropertyChanged(nameof(CurrentTower)); } }
        public List<UpgradePath> CurrentPath { get { return _currentPath; } set { _currentPath = value; OnPropertyChanged(nameof(CurrentPath)); } }
        public bool UseApi { get; set; } = true;

        //Commands
        public RelayCommand PreviousUpgradesCommand { get; private set; }
        public RelayCommand NextUpgradesCommand { get; private set; }

        private void LoadCurrentPath()
        {
            if (CurrentTower == null) return;

            //Check range
            if (CurrentTower.Paths.Count > _currentPathIndex && _currentPathIndex >= 0) 
            {
                CurrentPath = CurrentTower.Paths[_currentPathIndex];
            }
        }

        private async void LoadTower()
        {
            CurrentTower = await _bloomTDRepository.GetTowerAsync();

            //Load default path
            _currentPathIndex = 0;
            LoadCurrentPath();
        }


        public TowerPageVM()
        {
            LoadTower();

            PreviousUpgradesCommand = new RelayCommand(PreviousUpgrades);
            NextUpgradesCommand = new RelayCommand(NextUpgrades);
        }

        //Helper functions
        private void NextUpgrades()
        {
            if (CurrentTower == null) return;

            if (_currentPathIndex >= CurrentTower.Paths.Count -1)
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
    }
}
