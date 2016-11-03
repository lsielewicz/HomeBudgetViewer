using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace HomeBudgetViewer.Controls.InformationDialog
{
    public sealed class InformationDialogViewModel : ViewModelBase
    {
        private RelayCommand _hideDialogCommand;
        public string Message { get; private set; }
        private InformationDialog _viewInstance;
        public InformationDialogViewModel(InformationDialog viewInstance, string message)
        {
            this.Message = message;
            this._viewInstance = viewInstance;
            this.RaisePropertyChanged("Message");
        }

        public RelayCommand HideDialogCommand
        {
            get
            {
                return this._hideDialogCommand ?? (_hideDialogCommand = new RelayCommand(() =>
                {
                    this._viewInstance.Hide();
                }));
            }
        }
    }
}
