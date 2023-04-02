using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project_JanSupierz.Model;
using Project_JanSupierz.Repository;
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
        public string CommandText
        {
            get
            {
                if (CurrentPage is TowersPage)
                {
                    return "SHOW DETAILS";
                }
                else
                {
                    return "GO BACK";
                }
            }
        }

        public Page CurrentPage {  get; set; }
        private TowerPage TowerPage { get; } = new TowerPage();
        private TowersPage TowersPage { get; } = new TowersPage();

        public RelayCommand SwitchPageCommand { get; private set; }

        public MainWindowVM()
        {
            CurrentPage = TowersPage;
            SwitchPageCommand = new RelayCommand(SwitchPage);
        }

        public void SwitchPage()
        {
            if (CurrentPage is TowersPage)
            {
                Tower selectedTower = (TowersPage.DataContext as TowersPageVM).SelectedTower;
                if (selectedTower == null) return;

                (TowerPage.DataContext as TowerPageVM).CurrentTower = selectedTower;
                CurrentPage = TowerPage;
            }
            else
            {
                CurrentPage = TowersPage;
            }

            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(CommandText));
        }
    }
}
