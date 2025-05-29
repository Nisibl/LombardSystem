using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Database
{
    internal sealed class DatabaseSingleton
    {
        private static DatabaseSingleton _instance;
        private static readonly object _lock = new object();

        public NpgsqlConnection Connection { get; private set; }

        private DatabaseSingleton(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public static DatabaseSingleton GetInstance(string connectionString)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseSingleton(connectionString);
                    }
                }
            }
            return _instance;
        }

        public void Dispose()
        {
            Connection?.Close();
            Connection?.Dispose();
            _instance = null;
        }
    }
}
