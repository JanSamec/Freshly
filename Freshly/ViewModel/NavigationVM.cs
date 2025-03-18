using Freshly.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freshly.ViewModel
{
    class NavigationVM : Utilities.ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // Submenu state logic
        private bool _isBakerySubmenuOpen;
        public bool IsBakerySubmenuOpen
        {
            get { return _isBakerySubmenuOpen; }
            set
            {
                _isBakerySubmenuOpen = value;
                OnPropertyChanged();

                // Close other submenus when this one opens
                if (value)
                {
                    IsMeatSubmenuOpen = false;
                    IsFreshSubmenuOpen = false;
                }
            }
        }

        private bool _isMeatSubmenuOpen;
        public bool IsMeatSubmenuOpen
        {
            get { return _isMeatSubmenuOpen; }
            set
            {
                _isMeatSubmenuOpen = value;
                OnPropertyChanged();

                if (value)
                {
                    IsBakerySubmenuOpen = false;
                    IsFreshSubmenuOpen = false;
                }
            }
        }

        private bool _isFreshSubmenuOpen;
        public bool IsFreshSubmenuOpen
        {
            get { return _isFreshSubmenuOpen; }
            set
            {
                _isFreshSubmenuOpen = value;
                OnPropertyChanged();

                if (value)
                {
                    IsBakerySubmenuOpen = false;
                    IsMeatSubmenuOpen = false;
                }
            }
        }

        public ICommand HomeCommand { get; }
        public ICommand BakeryCommand { get; }
        public ICommand MeatCommand { get; }
        public ICommand FreshCommand { get; }
        public ICommand DairyCommand { get; }
        public ICommand DrinksCommand { get; }

        public ICommand Bakery1Command { get; }
        public ICommand Bakery2Command { get; }
        public ICommand Bakery3Command { get; }

        public ICommand Meat1Command { get; }
        public ICommand Meat2Command { get; }
        public ICommand Meat3Command { get; }

        public ICommand Fresh1Command { get; }
        public ICommand Fresh2Command { get; }

        public NavigationVM()
        { 

            HomeCommand = new RelayCommand(_ => NavigateTo<HomeVM>());
            BakeryCommand = new RelayCommand(_ => IsBakerySubmenuOpen = !IsBakerySubmenuOpen);
            MeatCommand = new RelayCommand(_ => IsMeatSubmenuOpen = !IsMeatSubmenuOpen);
            FreshCommand = new RelayCommand(_ => IsFreshSubmenuOpen = !IsFreshSubmenuOpen);
            DairyCommand = new RelayCommand(_ => NavigateTo<DairyVM>());
            DrinksCommand = new RelayCommand(_ => NavigateTo<DrinksVM>());

            Bakery1Command = new RelayCommand(_ => NavigateTo<Bakery1VM>());
            Bakery2Command = new RelayCommand(_ => NavigateTo<Bakery2VM>());
            Bakery3Command = new RelayCommand(_ => NavigateTo<Bakery3VM>());

            Meat1Command = new RelayCommand(_ => NavigateTo<Meat1VM>());
            Meat2Command = new RelayCommand(_ => NavigateTo<Meat2VM>());
            Meat3Command = new RelayCommand(_ => NavigateTo<Meat3VM>());

            Fresh1Command = new RelayCommand(_ => NavigateTo<Fresh1VM>());
            Fresh2Command = new RelayCommand(_ => NavigateTo<Fresh2VM>());

             // Inicial view to display
            NavigateTo<HomeVM>();
        }

        // Helper method for navigation
        private void NavigateTo<T>() where T : new()
        {
            CurrentView = new T();
        }
    }
}
