using GalaSoft.MvvmLight.Messaging;
using HomeBudgetViewer.Database.Engine.Entities;

namespace HomeBudgetViewer.Messages
{
    public class IsModifyingStateToBudgetItemMessage : MessageBase
    {
        public BudgetItem BudgetItem { get; private set; }

        public IsModifyingStateToBudgetItemMessage(BudgetItem budgetItem)
        {
            this.BudgetItem = budgetItem;
        }
    }
}
