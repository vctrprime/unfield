using System;
using System.Data;
using Npgsql;
using StadiumEngine.DataAccess.Connection.Abstract;

namespace StadiumEngine.DataAccess.Connection.Concrete
{
    public class PgConnectionCreator : IConnectionCreator
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        
        private bool _disposed;
        
        public PgConnectionCreator()
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }
        
        public IDbConnection Connection
        {
            get
            {
                _connection ??= Create();

                return _connection;
            }
        }
        
        
        private IDbConnection Create()
        {
            _connection = new NpgsqlConnection(_connectionString);

            _connection.Open();

            return _connection;
        }

       

        public void Dispose()
        {
            Cleanup();
            GC.SuppressFinalize(this);
        }
        
        private void Cleanup()
        {
            if (_disposed || _connection == null) return;
            
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            _connection.Dispose();

            _connection = null;
            
            _disposed = true;
        }
 
        ~PgConnectionCreator()
        {
            Cleanup();
        }
    }
}