using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using HomeBudgetViewer.Controls.Template10;
using HomeBudgetViewer.Presentation.SettingsPage.Tabs.About;
using HomeBudgetViewer.Presentation.SettingsPage.Tabs.Settings;
using HomeBudgetViewer.Services.SettingService;
using Template10.Mvvm;

namespace HomeBudgetViewer.Presentation.SettingsPage
{
    public class SettingsPageViewModel : AppViewModelBase
    {
        private SettingsPartViewModel _settingsPartViewModel;

        public SettingsPartViewModel SettingsPartViewModel
        {
            get { return _settingsPartViewModel ?? (_settingsPartViewModel = new SettingsPartViewModel(this)); } 
        }

        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();
    }



   
}
