using System;
using System.Linq;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;

namespace HomeBudgetViewer.Presentation.MainPage
{
    public class MainPageViewModel : AppViewModelBase
    {
        public MainPageViewModel()
        {
           TestDialog();
        }

        private async void TestDialog()
        {
            var dialog = new AddUserProfileDialog();
            await dialog.ShowAsync();
            switch (dialog.Result)
            {
                case UserProfileDialogResult.Ok:
                    break;
                case UserProfileDialogResult.Cancel:
                    break;
                case UserProfileDialogResult.UserExists:
                    break;
            }
        }
       
    }   
}
