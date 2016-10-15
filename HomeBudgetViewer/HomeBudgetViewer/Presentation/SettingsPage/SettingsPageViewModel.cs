using System;
using System.Collections.Generic;
using HomeBudgetViewer.Presentation.SettingsPage.Tabs.Settings;


namespace HomeBudgetViewer.Presentation.SettingsPage
{
    public class SettingsPageViewModel : AppViewModelBase
    {
        private SettingsPartViewModel _settingsPartViewModel;

        public SettingsPartViewModel SettingsPartViewModel
        {
            get { return _settingsPartViewModel ?? (_settingsPartViewModel = new SettingsPartViewModel(this)); } 
        }

    }



   
}
