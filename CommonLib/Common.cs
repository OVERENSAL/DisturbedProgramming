using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace CommonLib
{
    public interface IStorage
    {
        void Store(string key, string id, string text);
        string Load(string key, string id);
        List<string> GetKeys();
        bool IsKeyExist(string key);
        void SaveId(string id, string country);
    }

    public class Storage : IStorage
    {
        private readonly IConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(Constants.Host);


        public void Store(string key, string id, string text)
        {
            var db = _connection.GetDatabase();
            var secondDbName = db.StringGet(id);

            IConnectionMultiplexer secondConn = ConnectionMultiplexer.Connect(secondDbName);
            var secondDb = secondConn.GetDatabase();
            secondDb.StringSet(key, text);

            secondConn.Dispose();
            secondConn.Close();
        }

        public string Load(string key, string id)
        {
            var db = _connection.GetDatabase();
            var secondDbName = db.StringGet(id);

            IConnectionMultiplexer secondConn = ConnectionMultiplexer.Connect(secondDbName);
            var secondDb = secondConn.GetDatabase();

            return secondDb.StringGet(key);
        }

        public List<string> GetKeys()
        {
            var keys = _connection.GetServer(Constants.Host, Constants.Port).Keys();

            return keys.Select(item => item.ToString()).ToList();
        }

        public bool IsKeyExist(string key)
        {
            var db = _connection.GetDatabase();
            return db.KeyExists(key); 
        }

        public void SaveId(string id, string country)
        {
            var db = _connection.GetDatabase();
            db.StringSet(id, Constants.COUNTRIES_TO_REGIONS[country]);
        }

    }
}
