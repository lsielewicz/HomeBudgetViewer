using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.SettingsPage.Tabs.UserProfiles
{
    public class UserProfilesPartViewModel : AppViewModelBase
    {
        private SettingsService _settings;
        private AppViewModelBase _parentViewModel;
        private RelayCommand _addNewUserCommand;
        private RelayCommand _deleteSelectedUserCommand;
        private RelayCommand _updateSelectedUserCommand;
        private RelayCommand _navigateToUserSelectionPage;

        public UserProfilesPartViewModel(AppViewModelBase parentViewModel) : base()
        {
            _settings = SettingsService.Instance;
            _parentViewModel = parentViewModel;
        }

        public string CurrentUser
        {
            get { return _settings.CurrentUser.Name; }
        }

        public RelayCommand AddNewUserCommand
        {
            get
            {
                return _addNewUserCommand ?? (_addNewUserCommand = new RelayCommand(async () =>
                {
                    var dialog = new AddUserProfileDialog();
                    await dialog.ShowAsync();
                }));
            }
        }

        public RelayCommand DeleteSelectedUserCommand
        {
            get
            {
                return _deleteSelectedUserCommand ?? (_deleteSelectedUserCommand = new RelayCommand(() =>
                {
                    if (_settings.CurrentUser == null)
                        return;

                    using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                    {
                        unitOfWork.Users.Remove(_settings.CurrentUser);
                        unitOfWork.Complete();
                    }
                }));
            }
        }

        public RelayCommand UpdateSelectedUserCommand
        {
            get
            {
                return _updateSelectedUserCommand ?? (_updateSelectedUserCommand = new RelayCommand(async () =>
                {

                }));
            }
        }

        public RelayCommand NavigateToUserSelectionPage
        {
            get
            {
                return _navigateToUserSelectionPage ?? (_navigateToUserSelectionPage = new RelayCommand(() =>
                {
                    _parentViewModel.NavigationService.Navigate(typeof(UserSelectionPage.UserSelectionPage));
                }));
            }
        }

    }
}
