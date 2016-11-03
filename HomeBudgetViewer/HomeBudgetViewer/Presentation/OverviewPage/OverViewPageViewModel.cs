using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Models.Enum;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.OverviewPage
{
    public class OverviewPageViewModel : DateOperationViewModelBase
    {
        private ObservableCollection<BudgetItem> _currentItems;

        public OverviewPageViewModel() : base()
        {
        }

        public ObservableCollection<BudgetItem> CurrentItems
        {
            get { return _currentItems; }
            set
            {
                _currentItems = value;
                RaisePropertyChanged();
            }
        }

        public override void GetData()
        {
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                List<BudgetItem> items=null;
                GetDataByDate getDataMethod = null;
                if(this.BudgetItemType == ItemType.Expense && this.CurrentDateFilter == DateFilter.ByMonth)
                    getDataMethod = unitOfWork.BudgetItems.GetAllExpensesOfUserByMonth;
                else if(this.BudgetItemType == ItemType.Expense && this.CurrentDateFilter == DateFilter.ByDay)
                    getDataMethod = unitOfWork.BudgetItems.GetAllExpensesOfUserByDay;
                else if(this.BudgetItemType == ItemType.Income && this.CurrentDateFilter == DateFilter.ByMonth)
                    getDataMethod = unitOfWork.BudgetItems.GetAllIncomesOfUserByMonth;
                else
                    getDataMethod = unitOfWork.BudgetItems.GetAllIncomesOfUserByDay;
                
                items = getDataMethod(SettingsService.Instance.CurrentUser.Id, this.CurrentDateTime);

                CurrentItems = new ObservableCollection<BudgetItem>(items);
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            this.CurrentDateFilter = DateFilter.ByMonth;
            this.GetData();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

    }
}
