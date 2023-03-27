using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project_JanSupierz.Model;
using Project_JanSupierz.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.ViewModel
{
    public class TowerPageVM: ObservableObject
    {
        private string _currentPathName;
        private Tower _currentTower  = new Tower
        {
            Cost = new Cost { Easy = 170, Medium = 200, Hard = 215, Impoppable = 240 },
            Name = "DART MONKEY",
            Description = "Throws a single dart at nearby Bloons. Short range and low pierce but cheap.",
            Stats = new Statistics { AttackSpeed = "0.95", Damage = "1", Pierce = "2", Range = "32", Type = "Sharp" },
            Type = "Primary",
            Footprint = 6,
            DefaultHotkey = "Q",
            Id = "dart-monkey"
        };
        public Tower CurrentTower { get { return _currentTower; } set { _currentTower = value;  OnPropertyChanged(nameof(CurrentTower)); } }

        public List<PathUpgrade> CurrentPath { get; set; } = new List<PathUpgrade>();

        public RelayCommand PreviousUpgradesCommand { get; private set; }
        public RelayCommand NextUpgradesCommand { get; private set; }

        public void NextUpgrades()
        {
            if(_currentPathName == null)
            {
                _currentPathName = "path1";
            }
            else
            {
                //Char to int conversion
                int lastLetter = _currentPathName.Last() - '0';

                if(lastLetter == 3)
                {
                    lastLetter = 1;
                }
                else
                {
                    ++lastLetter;
                }

                _currentPathName = $"path{lastLetter}";
            }

            CurrentPath = CurrentTower.Paths[_currentPathName];
            OnPropertyChanged(nameof(CurrentPath));
        }

        public void PreviousUpgrades()
        {
            if (_currentPathName == null)
            {
                _currentPathName = "path1";
            }
            else
            {
                //Char to int conversion
                int lastLetter = _currentPathName.Last() - '0';

                if (lastLetter == 1)
                {
                    lastLetter = 3;
                }
                else
                {
                    --lastLetter;
                }

                _currentPathName = $"path{lastLetter}";
            }

            CurrentPath = CurrentTower.Paths[_currentPathName];
            OnPropertyChanged(nameof(CurrentPath));
        }
        public TowerPageVM()
        {
            CurrentTower = ReposioryLocal.GetTowers()[5];
            PreviousUpgradesCommand = new RelayCommand(PreviousUpgrades);
            NextUpgradesCommand = new RelayCommand(NextUpgrades);
            NextUpgrades();
        }
    }
}
