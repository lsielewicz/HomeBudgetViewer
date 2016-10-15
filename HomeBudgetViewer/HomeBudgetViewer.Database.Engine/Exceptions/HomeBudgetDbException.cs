using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudgetViewer.Database.Engine.Exceptions
{
    public class HomeBudgetDbException : Exception
    {
        public HomeBudgetDbException(string message) : base(message)
        {
        }
    }
}
