using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Currency;
using HomeBudgetViewer.Presentation;

namespace HomeBudgetViewer.Controls.AddUserProfileDialog
{
    public class AddUserProfileDialogViewModel : AppViewModelBase
    {
        private RelayCommand _okButtonCommand;
        private RelayCommand _cancelButtonCommand;
        private string _profileName;
        private CurrencyModel _profileCurrency;
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
                    if (string.IsNullOrEmpty(ProfileName) || this.ProfileCurrency == null)
                    {
                        this.ValidationMessage = this.GetLocalizedString("ValuesCannotBeEmpty");
                        return;
                    }
                    bool result = this.AddUserToDataBase();
                    if (!result)
                    {
                        this.ValidationMessage = this.GetLocalizedString("UserAlreadyExist");
                        return;
                    }

                    this.Result = UserProfileDialogResult.Ok;
                    this._dialog.Hide();
                }
                ));
            }
        }

        private bool AddUserToDataBase()
        {
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                bool userExist = unitOfWork.Users.CheckIfUserNameExists(this.ProfileName);
                if (userExist)
                    return false;
                unitOfWork.Users.Add(new User()
                {
                    Name = this.ProfileName,
                    Currency = this.ProfileCurrency.CurrencyEnum.ToString()
                });
                unitOfWork.Complete();
            }
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

        private ObservableCollection<CurrencyModel> _currencies;
        public ObservableCollection<CurrencyModel> Currencies
        {
            get { return _currencies ?? (_currencies = new ObservableCollection<CurrencyModel>(CurrencyModel.PossibleCurrencies.Take(5))); }
        }

    }
}
