using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Presentation;

namespace HomeBudgetViewer.Controls.ConfirmationDialog
{
    public class ConfirmationDialogViewModel : AppViewModelBase
    {
        private string _dialogMessage;
        private RelayCommand<object> _setDialogResult;
        private ConfirmationDialog _viewInstance;
        public ConfirmationDialogViewModel(ConfirmationDialog viewInstance, string message)
        {
            this._viewInstance = viewInstance;
            this.DialogMessage = message;
        }

        public ConfirmationDialogResult DialogResult
        {
            get { return _viewInstance.ConfirmationDialogResult; }
            set { _viewInstance.ConfirmationDialogResult = value; }
        }

        public string DialogMessage
        {
            get
            {
                return _dialogMessage;
            }
            set
            {
                if (_dialogMessage == value)
                    return;
                _dialogMessage = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand<object> SetDialogResult
        {
            get
            {
                return _setDialogResult ?? (_setDialogResult = new RelayCommand<object>(param =>
                {
                    if (param != null)
                    {
                        var result = (ConfirmationDialogResult) param;
                        this.DialogResult = result;
                        _viewInstance.Hide();
                    }
                }));
            }
        }
    }
}
