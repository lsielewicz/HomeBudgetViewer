using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.UI.Xaml;
using HomeBudgetViewer.Controls.Template10;
using HomeBudgetViewer.Services.SettingService;
using Template10.Mvvm;

namespace HomeBudgetViewer.Presentation.SettingsPage.Tabs.Settings
{
    public class SettingsPartViewModel : AppViewModelBase
    {
        private readonly AppViewModelBase _parentViewModel;
        private readonly SettingsService _settings;

        public SettingsPartViewModel(AppViewModelBase parentViewModel) : base()
        {
            _settings = SettingsService.Instance;
            _parentViewModel = parentViewModel;
        }

        public bool UseShellBackButton
        {
            get { return _settings.UseShellBackButton; }
            set
            {
                _settings.UseShellBackButton = value;
                this.RaisePropertyChanged();
            }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set
            {
                _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark;
                this.RaisePropertyChanged();
            }
        }

        public string CurrentLanguage
        {
            get
            {
                return GetCurrentLanguage();
            }
            set
            {
                SetCurrentLanguage(value);
                _parentViewModel.NavigationService.Navigate(typeof(SettingsPage));
                this.RaisePropertyChanged();
            }
        }

        private void SetCurrentLanguage(string item)
        {
            if (item == this.GetLocalizedString("Polish"))
            {
                _settings.CurrentLanguage = ApplicationLanguage.Polish;
                ApplicationLanguages.PrimaryLanguageOverride = ApplicationLanguage.Polish;
            }
               
            if (item == this.GetLocalizedString("English"))
            {
                _settings.CurrentLanguage = ApplicationLanguage.English;
                ApplicationLanguages.PrimaryLanguageOverride = ApplicationLanguage.English;
            }
             
        }

        private string GetCurrentLanguage()
        {
            var item = _settings.CurrentLanguage;
            if (item == ApplicationLanguage.Polish)
                return this.GetLocalizedString("Polish");
            else
                return this.GetLocalizedString("English");
        }

    }

}
