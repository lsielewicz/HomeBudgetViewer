using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.SettingsPage.Tabs.UserProfiles.UserSelectionPage
{
    public class UserSelectionPageVIewModel : AppViewModelBase
    {
        private RelayCommand _setSelectedUserAsCurrentUserCommand;
        private ObservableCollection<User> _users;
        public User SelectedUser { get; set; }

        public UserSelectionPageVIewModel()
        {
            
        }

        private async void AddNewUser()
        {
            var dialog = new AddUserProfileDialog();
            await dialog.ShowAsync();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            this.GetUsers();
            if (!this.Users.Any())
            {
                this.AddNewUser();
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public ObservableCollection<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                RaisePropertyChanged();
            }
        }

        private void GetUsers()
        {
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                var users = unitOfWork.Users.GetAll();
                if(SettingsService.Instance.CurrentUser != null)
                    users = users.ToList().Where(u => u.Id != SettingsService.Instance.CurrentUser.Id);
                Users = new ObservableCollection<User>(users);
            }
        }

        public RelayCommand SetSelectedUserAsCurrentUserCommand
        {
            get
            {
                return _setSelectedUserAsCurrentUserCommand ?? (_setSelectedUserAsCurrentUserCommand = new RelayCommand(
                    () =>
                    {
                        if (this.SelectedUser != null)
                        {
                            SettingsService.Instance.CurrentUser = this.SelectedUser;
                        }
                        this.NavigationService.Navigate(typeof(SettingsPage));
                    }));
            }
        }
    }
}
