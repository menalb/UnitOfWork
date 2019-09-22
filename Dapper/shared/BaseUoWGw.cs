using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace shared
{
    public abstract class BaseUoWGw
    {
        private readonly IDbConnection _connection;
        private readonly IUnitOfWork _uow;
        public BaseUoWGw(IDbConnection connection)
        {
            _connection = connection;
        }

        public BaseUoWGw(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected IDbConnection GetConnection()
        {
            return _uow == null ? _connection : _uow.GetConnection();
        }

        protected IDbTransaction GetTransaction()
        {
            return _uow?.GetTransaction();
        }

        protected Task ExecuteAsync(string command, object param = null)
        {
            return GetConnection().ExecuteAsync(command, param, GetTransaction());
        }
    }
}
