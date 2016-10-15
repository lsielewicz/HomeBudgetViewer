using System;
using System.Collections.Generic;
using HomeBudgetViewer.Presentation.SettingsPage.Tabs.Settings;
using HomeBudgetViewer.Presentation.SettingsPage.Tabs.UserProfiles;


namespace HomeBudgetViewer.Presentation.SettingsPage
{
    public class SettingsPageViewModel : AppViewModelBase
    {
        private SettingsPartViewModel _settingsPartViewModel;
        private UserProfilesPartViewModel _userProfilesPartViewModel;
        public SettingsPartViewModel SettingsPartViewModel
        {
            get
            {
                return _settingsPartViewModel ?? (_settingsPartViewModel = new SettingsPartViewModel(this));
            } 
        }

        public UserProfilesPartViewModel UserProfilesPartViewModel
        {
            get
            {
                return _userProfilesPartViewModel ?? (_userProfilesPartViewModel = new UserProfilesPartViewModel(this));
            }
        }

    }



   
}
