using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leankit.Helper
{
    class Dictionary
    {
        public static Dictionary<string, string> AddToDict(Dictionary<string, string> _dict, string _key, string _value)
        {
            _dict[_key] = _value;
            return _dict;
        }

        public static string RetrieveFromDict(Dictionary<string, string> _inputDict, string _keyName)
        {
            string _outValue = null;
            _inputDict.TryGetValue(_keyName, out _outValue);

            return _outValue;
        }
    }
}
