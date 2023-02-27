using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive
{
    public static class ConfigurationProvider
    {
        private static string FilePath { get => Path.Combine(System.Environment.CurrentDirectory, "user.settings"); }

        public static SerializableWakuLiveConfiguration Load()
        {
            SerializableWakuLiveConfiguration config = null;
            config = SerializeUtility.LoadXml<SerializableWakuLiveConfiguration>(FilePath);

            return config;
        }

        public static void Save(SerializableWakuLiveConfiguration config)
        {
            SerializeUtility.SaveXml<SerializableWakuLiveConfiguration>(config, FilePath);
        }
    }
}
