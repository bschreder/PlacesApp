using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Library.Helpers
{
    public static class ToKeyValuePair
    {
        public static KeyValuePair<string, T> FromValueType<T>(T property)
            where T: struct
        {
            Type t = property.GetType();
            var key = t.Name;
            var kvp = new KeyValuePair<string, T>(key, property);
            return kvp;
        }
    }
}
