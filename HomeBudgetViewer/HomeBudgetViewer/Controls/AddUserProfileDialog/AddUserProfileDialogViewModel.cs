using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Presentation;

namespace HomeBudgetViewer.Controls.AddUserProfileDialog
{
    public class AddUserProfileDialogViewModel : AppViewModelBase
    {
        private RelayCommand _okButtonCommand;
        private RelayCommand _cancelButtonCommand;
        private string _profileName;
        private string _profileCurrency;
        private string _validationMessage;

        private AddUserProfileDialog _dialog;
        public User User { get; private set; }
        public UserProfileDialogResult Result
        {
            get { return this._dialog.Result; }
            set { this._dialog.Result = value; }
        }

        public AddUserProfileDialogViewModel(AddUserProfileDialog dialog)
        {
            this._dialog = dialog;
        }

        public RelayCommand OkButtonCommand
        {
            get
            {
                return _okButtonCommand ?? (_okButtonCommand = new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(ProfileName) || string.IsNullOrEmpty(ProfileCurrency))
                    {
                        this.ValidationMessage = this.GetLocalizedString("ValuesCannotBeEmpty");
                        return;
                    }

                    this.Result = UserProfileDialogResult.Ok;
                    this._dialog.Hide();
                }
                ));
            }
        }

        public RelayCommand CancelButtonCommand
        {
            get
            {
                return _cancelButtonCommand ?? (_cancelButtonCommand = new RelayCommand(() =>
                {
                    this.Result = UserProfileDialogResult.Cancel;
                    this._dialog.Hide();
                }));
            }
        }

        public string ProfileName
        {
            get
            {
                return _profileName;
            }
            set
            {
                if (_profileName == value)
                    return;
                _profileName = value;
                this.RaisePropertyChanged();
            }
        }

        public string ProfileCurrency
        {
            get
            {
                return _profileCurrency;
            }
            set
            {
                if (_profileCurrency == value)
                    return;
                _profileCurrency = value;
                this.RaisePropertyChanged();
            }
        }
        public string ValidationMessage
        {
            get
            {
                return _validationMessage;
            }
            set
            {
                if (_validationMessage == value)
                    return;
                _validationMessage = value;
                this.RaisePropertyChanged();
            }
        }

    }
}
