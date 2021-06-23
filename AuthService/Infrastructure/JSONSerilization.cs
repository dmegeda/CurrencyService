using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure
{
    public static class JSONSerialization<T> where T : class
    {
        public static void Serialize(List<T> data, string path)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T[]));
            using FileStream fs = new FileStream(path, FileMode.Create);
            json.WriteObject(fs, data.ToArray());
        }

        public static List<T> Deserialize(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            byte[] array = new byte[fileStream.Length];
            fileStream.Read(array, 0, array.Length);
            string json = Encoding.Default.GetString(array);
            fileStream.Close();

            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
