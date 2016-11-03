using System;

namespace HomeBudgetViewer.Database.Engine.Exceptions
{
    public class HomeBudgetDbException : Exception
    {
        public HomeBudgetDbException(string message) : base(message)
        {
        }
    }
}
