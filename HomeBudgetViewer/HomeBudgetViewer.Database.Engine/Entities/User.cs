using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudgetViewer.Database.Engine.Entities
{
    public class User : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public string Currency { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public User()
        {
            BudgetItems = new List<BudgetItem>();
        }

        public void AddBudgetItemToList(BudgetItem item)
        {
            item.User = this;
            this.BudgetItems.Add(item);
        }
    }
}
