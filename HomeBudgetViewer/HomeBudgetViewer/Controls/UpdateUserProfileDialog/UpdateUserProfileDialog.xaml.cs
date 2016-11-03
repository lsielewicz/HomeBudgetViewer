using Windows.UI.Xaml.Controls;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Entities;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeBudgetViewer.Controls.UpdateUserProfileDialog
{
    public sealed partial class UpdateUserProfileDialog : ContentDialog
    {
        public UserProfileDialogResult Result { get; set; }
        public User UpdatedUser { get; set; }
        public UpdateUserProfileDialog(User user)
        {
            this.InitializeComponent();
            this.DataContext = new UpdateUserProfileDialogViewModel(this, user);
            this.Loaded += (s, e) =>
            {
                this.ComboBox.SelectedIndex = 0;
            };
        }

       
    }
}
