using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly SettingsService _settings;
        private readonly AppViewModelBase _parentViewModel;
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
            get { return _settings.CurrentUser.Name ?? string.Empty; }
        }

        public RelayCommand AddNewUserCommand
        {
            get
            {
                return _addNewUserCommand ?? (_addNewUserCommand = new RelayCommand(async () =>
                {
                    var dialog = new AddUserProfileDialog();
                    await dialog.ShowAsync();
                    _parentViewModel.NavigationService.Navigate(typeof(UserSelectionPage.UserSelectionPage));
                }));
            }
        }

        public RelayCommand DeleteSelectedUserCommand
        {
            get
            {
                return _deleteSelectedUserCommand ?? (_deleteSelectedUserCommand = new RelayCommand(() =>
                {
                    if (_settings.CurrentUser == null || string.IsNullOrEmpty(_settings.CurrentUser.Name))
                        return;

                    bool isDbEmpty = true;
                    using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                    {
                        unitOfWork.Users.Remove(_settings.CurrentUser);
                        unitOfWork.Complete();
                        _settings.CurrentUser = null;
                        isDbEmpty = !unitOfWork.Users.GetAll().Any();
                    }
                    if(isDbEmpty)
                        this.AddNewUserCommand.Execute(this);
                    else
                        _parentViewModel.NavigationService.Navigate(typeof(UserSelectionPage.UserSelectionPage));
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
