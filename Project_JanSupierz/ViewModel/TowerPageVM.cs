using CommunityToolkit.Mvvm.ComponentModel;
using Project_JanSupierz.Model;
using Project_JanSupierz.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.ViewModel
{
    public class TowerPageVM: ObservableObject
    {
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
        public Tower CurrentTower { get { return _currentTower; } set { _currentTower = value; OnPropertyChanged(nameof(CurrentTower)); } }

        public List<PathUpgrade> CurrentPath { get; set; } = new List<PathUpgrade>();

        public TowerPageVM()
        {
            CurrentTower = ReposioryLocal.GetTowers()[3];
            CurrentPath = CurrentTower.Paths["path1"];
        }
    }
}
