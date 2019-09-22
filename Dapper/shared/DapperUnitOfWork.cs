using System;
using System.Data;

namespace shared
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        IDbConnection _connection;
        IDbTransaction _transaction;
        bool _ownCOnnection;
        public DapperUnitOfWork(IDbConnection connection, bool ownsConnection)
        {
            _connection = connection;
            _ownCOnnection = ownsConnection;
            _transaction = _connection.BeginTransaction();
        }

        public IDbConnection GetConnection()
        {
            return _connection;
        }

        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
            if (_connection != null && _ownCOnnection)
            {
                _connection.Close();
                _connection = null;
            }
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("transaction already committed");

            _transaction.Commit();
            _transaction = null;
        }
    }
}
