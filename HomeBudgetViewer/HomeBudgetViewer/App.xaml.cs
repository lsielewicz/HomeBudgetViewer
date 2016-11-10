using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Controls.ConfirmationDialog;
using HomeBudgetViewer.Controls.InformationDialog;
using HomeBudgetViewer.Controls.Template10;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Services.SettingService;
using Template10.Common;
using Template10.Controls;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetViewer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : BootStrapper
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Splash(e);

            SettingsService.Instance.InitializeStartupSettings(this);

            using (var db = new BudgetContext())
            {
                db.Database.Migrate();
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            if (!(Window.Current.Content is ModalDialog))
            {
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);

                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Shell(nav),
                    ModalContent = new Busy(),
                };
            }

            while (this.IsDbEmpty())
            {
                var dialog = new AddUserProfileDialog();
                await dialog.ShowAsync();
                if (string.IsNullOrEmpty(SettingsService.Instance.CurrentUser.Name))
                {
                    using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                    {
                        var user = unitOfWork.Users.GetAll().FirstOrDefault();
                        if (user != null)
                        {
                            SettingsService.Instance.CurrentUser = user;
                        }
                    }
                }
                if (dialog.Result == UserProfileDialogResult.Cancel)
                {
                    var confirmationDialog = new InformationDialog(ResourceLoader.GetForViewIndependentUse().GetString("AtLeastOneUserProfile"));
                    await confirmationDialog.ShowAsync();      
                }
            }

            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
           
           
            NavigationService.Navigate(typeof(MainPage));

            await Task.CompletedTask;
        }

        private bool IsDbEmpty()
        {
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                return !unitOfWork.Users.GetAll().Any();
            }    
        }

    }
}
