using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Valuator
{
    public class Storage : IStorage
    {
        private const string Host = "localhost";
        private const int Port = 6379;
        private readonly IConnectionMultiplexer _connection;
 
        public Storage()
        {
            _connection = ConnectionMultiplexer.Connect(Host);
        }

        public void Store(string key, string text)
        {
            var db = _connection.GetDatabase();
            db.StringSet(key, text);
        }

        public string Load(string key)
        {
            var db = _connection.GetDatabase();
            return db.StringGet(key);
        }

        public List<string> GetKeys()
        {
            var keys = _connection.GetServer(Host, Port).Keys();

            return keys.Select(item => item.ToString()).ToList();
        }
    }
}
