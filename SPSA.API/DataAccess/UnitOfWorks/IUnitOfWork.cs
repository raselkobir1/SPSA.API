﻿using SPSA.API.DataAccess.Interfaces;

namespace SPSA.API.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChange(); 
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        long GetLoggedInUserId();
        (bool, string) HasDependency(string table, string id);

        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        ITokenRepository Tokens { get; }
        IMenuRepository Menus { get; }  
    }
}
