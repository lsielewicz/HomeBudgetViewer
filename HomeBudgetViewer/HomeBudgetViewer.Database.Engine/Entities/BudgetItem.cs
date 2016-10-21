using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudgetViewer.Database.Engine.Entities
{
    public class BudgetItem : EntityBase
    {
        public double MoneyValue { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ItemType { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
    }
}
