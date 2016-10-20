using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Presentation.BudgetItemPage.CategorySelectionPage;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.MainPage
{
    public class MainPageViewModel : AppViewModelBase
    {

        public MainPageViewModel()
        {
           
        }

        public string HelloMessage
        {
            get { return $"{this.GetLocalizedString("Hello")} {SettingsService.Instance.CurrentUser.Name}"; }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //this.NavigationService.Navigate(typeof(BudgetItemPage.BudgetItemPage));
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }   
}
