using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WakuLive
{
    public static class SerializeUtility
    {
        public static void SaveXml<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static T LoadXml<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return default(T);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(fs);
            }
        }
    }
}
