using System;
using System.Linq;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
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
       
    }   
}
