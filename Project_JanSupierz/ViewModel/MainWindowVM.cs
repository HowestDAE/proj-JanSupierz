using CommunityToolkit.Mvvm.ComponentModel;
using Project_JanSupierz.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_JanSupierz.ViewModel
{
    public class MainWindowVM: ObservableObject
    {
        public Page CurrentPage {  get; set; }
        private TowerPage TowerPage { get; } = new TowerPage();
        private TowersPage TowersPage { get; } = new TowersPage();

        public MainWindowVM()
        {
            CurrentPage = TowersPage;
        }
    }
}
