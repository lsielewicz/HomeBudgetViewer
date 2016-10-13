using System;
using Windows.UI.Xaml;
using HomeBudgetViewer.Controls.Template10;
using Template10.Common;
using Template10.Services.SettingsService;
using Template10.Utils;

namespace HomeBudgetViewer.Services.SettingService
{
    public class SettingsService
    {
        public static SettingsService Instance { get; } = new SettingsService();
        private ISettingsHelper _helper;
        private SettingsService()
        {
            _helper = new SettingsHelper();
        }

        public bool UseShellBackButton
        {
            get
            {
                return _helper.Read<bool>(nameof(UseShellBackButton), true);
            }
            set
            {
                _helper.Write(nameof(UseShellBackButton), value);
                BootStrapper.Current.NavigationService.Dispatcher.Dispatch(() =>
                {
                    BootStrapper.Current.ShowShellBackButton = value;
                    BootStrapper.Current.UpdateShellBackButton();
                    BootStrapper.Current.NavigationService.Refresh();
                });
            }
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Light;
                var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());
                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }
            set
            {
                _helper.Write(nameof(AppTheme), value.ToString());
                var frameworkElement = Window.Current.Content as FrameworkElement;
                if (frameworkElement != null)
                {
                    frameworkElement.RequestedTheme = value.ToElementTheme();
                    Shell.HamburgerMenu.RefreshStyles(value);
                }
            }
        }

        public TimeSpan CacheMaxDuration
        {
            get
            {
                return _helper.Read<TimeSpan>(nameof(CacheMaxDuration), TimeSpan.FromDays(2));
            }
            set
            {
                _helper.Write(nameof(CacheMaxDuration), value);
                BootStrapper.Current.CacheMaxDuration = value;
            }
        }

        public string CurrentLanguage
        {
            get
            {
                return _helper.Read<string>(nameof(CurrentLanguage), "en");
            }
            set
            {
                _helper.Write(nameof(CurrentLanguage),value);
            }
        }
    }
}
