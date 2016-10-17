using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Currency;
using HomeBudgetViewer.Presentation;

namespace HomeBudgetViewer.Controls.UpdateUserProfileDialog
{
    public class UpdateUserProfileDialogViewModel : AppViewModelBase
    {
        private RelayCommand _okButtonCommand;
        private RelayCommand _cancelButtonCommand;
        private string _profileName;
        private CurrencyModel _profileCurrency;
        private string _validationMessage;
        private User _user;
        private readonly UpdateUserProfileDialog _dialog;
        public UserProfileDialogResult Result
        {
            get { return this._dialog.Result; }
            set { this._dialog.Result = value; }
        }

        public UpdateUserProfileDialogViewModel(UpdateUserProfileDialog dialog, User user)
        {
            this._dialog = dialog;
            this._user = user;
            this.ProfileName = _user.Name;
            this.ProfileCurrency = Currencies.FirstOrDefault(c=>c.CurrencyName == _user.Currency);
        }

        public RelayCommand OkButtonCommand
        {
            get
            {
                return _okButtonCommand ?? (_okButtonCommand = new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(ProfileName) || this.ProfileCurrency == null)
                    {
                        this.ValidationMessage = this.GetLocalizedString("ValuesCannotBeEmpty");
                        return;
                    }
                    bool result = this.UpdateUserInDb();
                    if (!result)
                    {
                        this.ValidationMessage = this.GetLocalizedString("GivenNameAlreadyRegistered");
                        return;
                    }

                    this.Result = UserProfileDialogResult.Ok;
                    this._dialog.Hide();
                }
                ));
            }
        }

        private bool UpdateUserInDb()
        {
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                if (this.ProfileName != _user.Name && unitOfWork.Users.CheckIfUserNameExists(this.ProfileName))
                    return false;
                _user.Name = this.ProfileName;
                _user.Currency = this.ProfileCurrency.CurrencyName;
                unitOfWork.Users.Update(_user);
                unitOfWork.Complete();
            }
            this._dialog.UpdatedUser = _user;
            return true;
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

        public CurrencyModel ProfileCurrency
        {
            get
            {
                return _profileCurrency ?? (_profileCurrency = Currencies.FirstOrDefault());
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

        public List<CurrencyModel> Currencies
        {
            get { return CurrencyModel.PossibleCurrencies; }
        }
    }
}
