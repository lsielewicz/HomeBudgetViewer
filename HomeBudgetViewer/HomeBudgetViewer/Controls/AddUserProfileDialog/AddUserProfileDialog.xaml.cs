using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeBudgetViewer.Controls.AddUserProfileDialog
{
    public sealed partial class AddUserProfileDialog : ContentDialog
    {
        public UserProfileDialogResult Result { get; set; }
        public AddUserProfileDialog()
        {
            //var viewModel = new AddUserProfileDialogViewModel(this);
            this.InitializeComponent();
            this.DataContext = new AddUserProfileDialogViewModel(this);
            this.Loaded += (s, e) =>
            {
                this.ComboBox.SelectedIndex = 0;
            };

        }

    }
}
