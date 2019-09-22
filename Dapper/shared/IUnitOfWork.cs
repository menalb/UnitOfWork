using System;
using System.Data;

namespace shared
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection GetConnection();
        IDbTransaction GetTransaction();
        void SaveChanges();
    }
}
