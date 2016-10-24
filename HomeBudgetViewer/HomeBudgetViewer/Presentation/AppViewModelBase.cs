using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight;
using HomeBudgetViewer.Services.SettingService;
using Template10.Common;
using Template10.Services.NavigationService;

namespace HomeBudgetViewer.Presentation
{
    public abstract class AppViewModelBase : ViewModelBase, INavigable
    {
        protected AppViewModelBase()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();
        }

        public virtual Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            return Task.CompletedTask;
        }

        public INavigationService NavigationService { get; set; }
        public IDispatcherWrapper Dispatcher { get; set; }
        public IStateItems SessionState { get; set; }

        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return this._isLoading;
            }
            set
            {
                if (this._isLoading == value)
                    return;
                this._isLoading = value;
                this.RaisePropertyChanged();
            }
        }

        private readonly ResourceLoader _resourceLoader;
        public string GetLocalizedString(string resourceKey)
        {
            return this._resourceLoader.GetString(resourceKey);
        }

        public string GetLocalizedMonth(int monthIndex)
        {
            return this.GetLocalizedString($"Month{monthIndex}");
        }

        public string GetMoneyValue(double moneyValue)
        {
            NumberFormatInfo nfi = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return moneyValue.ToString("#,0.00", nfi);
        }


    }
}
