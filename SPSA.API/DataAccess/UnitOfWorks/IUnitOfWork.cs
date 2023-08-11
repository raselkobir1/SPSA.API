namespace SPSA.API.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChange(); 
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        long GetLoggedInUserId();
        (bool, string) HasDependency(string table, string id);


    }
}
