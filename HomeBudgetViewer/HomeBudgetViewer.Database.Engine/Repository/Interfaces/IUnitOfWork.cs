﻿using System;

namespace HomeBudgetViewer.Database.Engine.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IBudgetItemRepository BudgetItems { get; }
        int Complete();
    }
}