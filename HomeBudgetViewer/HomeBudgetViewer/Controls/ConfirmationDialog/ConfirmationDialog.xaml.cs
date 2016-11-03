using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeBudgetViewer.Controls.ConfirmationDialog
{
    public sealed partial class ConfirmationDialog : ContentDialog
    {
        


        public ConfirmationDialogResult ConfirmationDialogResult { get; set; }

        public ConfirmationDialog(string message)
        {
            this.InitializeComponent();
            this.DataContext = new ConfirmationDialogViewModel(this,message);
        }

    }
}
