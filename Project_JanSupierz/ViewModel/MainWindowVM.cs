using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project_JanSupierz.Model;
using Project_JanSupierz.Repository;
using Project_JanSupierz.View;
using Project_JanSupierz.View.Converters;
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
        private BloonsTDApiRepository _bloonsTDApiRepository = new BloonsTDApiRepository();
        private BloonsTDLocalRepository _bloonsTDLocalRepository = new BloonsTDLocalRepository();
        private IBloonsTDRepository _bloonsTDRepository = null;

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

            //Select a repository -- _bloonsTDLocalRepository or _bloonsTDApiRepository
            SetRepository(_bloonsTDLocalRepository);
            (TowersPage.DataContext as TowersPageVM).Load(new RelayCommand(ChangeRepository));
        }

        private void ChangeRepository()
        {
            if(_bloonsTDRepository == _bloonsTDApiRepository)
            {
                SetRepository(_bloonsTDLocalRepository);
            }
            else
            {
                SetRepository(_bloonsTDApiRepository);
            }

            //Reload the main page
            (TowersPage.DataContext as TowersPageVM).Load();
        }

        private void SetRepository(IBloonsTDRepository repository)
        {
            _bloonsTDRepository = repository;

            //Changing mode for images
            BloonsConverter.UseApi = (_bloonsTDRepository == _bloonsTDApiRepository);

            //Setting correct repository per page
            (TowerPage.DataContext as TowerPageVM).Repository = _bloonsTDRepository;
            (TowersPage.DataContext as TowersPageVM).Repository = _bloonsTDRepository;
        }

        private void SwitchPage()
        {
            if (CurrentPage is TowersPage)
            {
                TowersPageVM overviewVM = (TowersPage.DataContext as TowersPageVM);
                Tower selectedTower = overviewVM.SelectedTower;
                if (selectedTower == null) return;

                TowerPageVM detailsVM = (TowerPage.DataContext as TowerPageVM);
                detailsVM.CurrentTower = (Tower)(selectedTower).Clone();
                detailsVM.Id = selectedTower.Id;
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
