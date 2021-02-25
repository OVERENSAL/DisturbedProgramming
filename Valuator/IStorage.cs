using System.Collections.Generic;

namespace Valuator
{
    public interface IStorage
    {
        void Store(string key, string text);
        string Load(string key);
        List<string> GetKeys();
    }
}