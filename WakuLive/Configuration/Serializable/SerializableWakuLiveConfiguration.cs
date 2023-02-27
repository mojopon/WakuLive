using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive
{
    public class SerializableWakuLiveConfiguration
    {
        public SerializableAccountConfiguration Account = new SerializableAccountConfiguration();

        public static SerializableWakuLiveConfiguration CreateDefault() 
        {
            var config = new SerializableWakuLiveConfiguration();
            return config;
        }
    }
}
