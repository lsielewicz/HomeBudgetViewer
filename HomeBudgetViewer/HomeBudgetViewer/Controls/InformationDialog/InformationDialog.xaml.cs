using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeBudgetViewer.Controls.InformationDialog
{
    public sealed partial class InformationDialog : ContentDialog
    {
        public InformationDialog(string message)
        {
            this.InitializeComponent();
            this.DataContext = new InformationDialogViewModel(this,message);
        }

    }
}
