using System;
using System.Collections.Generic;   
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;
using HomeBudgetViewer.Models.Enum;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.Statistics
{
    public class StatisticsPageViewModel : DateOperationViewModelBase
    {
        public List<KeyValuePair<string,int>> ChartCollection { get; private set; }
        
        public override async void GetData()
        {
            this.IsLoading = true;
            int currentUserId = SettingsService.Instance.CurrentUser.Id;   
            this.ChartCollection = new List<KeyValuePair<string, int>>();
            await Task.Run(() =>
            {
                Parallel.ForEach(CategoryModel.PossibleCategories, async category =>
                {
                    using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                    {
                        var categoryName = this.GetLocalizedString(category.CategoryEnum.ToString());
                        var categorySum = unitOfWork.BudgetItems.GetSumOfItemsByCategory(
                            currentUserId,
                            this.CurrentDateTime,
                            this.BudgetItemType,
                            this.CurrentDateFilter,
                            category.CategoryEnum);

                        if (Math.Abs(categorySum) < 1)
                            return;

                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            ChartCollection.Add(new KeyValuePair<string, int>(categoryName, Convert.ToInt32(categorySum)));
                        });
                    }
                });
            });

            this.RaisePropertyChanged("ChartCollection");
            this.IsLoading = false;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            this.CurrentDateFilter = DateFilter.ByMonth;
            this.RaisePropertyChanged("CurrentDateFilter");
            this.RaisePropertyChanged("CurrentDateHeader");
            this.RaisePropertyChanged("CurrentCurrencyString");
            this.GetData();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
