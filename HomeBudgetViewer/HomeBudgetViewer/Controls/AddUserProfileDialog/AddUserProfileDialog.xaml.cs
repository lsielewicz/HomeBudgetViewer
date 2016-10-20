using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HomeBudgetViewer.Database.Engine.Entities;

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
