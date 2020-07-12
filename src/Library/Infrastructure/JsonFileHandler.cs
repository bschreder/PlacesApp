using Newtonsoft.Json;
using System;
using System.IO;

namespace Library.Infrastructure
{
    public class JsonFileHandler
    {
        public T ReadJson<T>(string file)
            where T : class, new()
        {
            var fileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);

            if (!File.Exists(fileLocation))
                return null;

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                T item = JsonConvert.DeserializeObject<T>(json);
                return item;
            }
        }
    }
}
