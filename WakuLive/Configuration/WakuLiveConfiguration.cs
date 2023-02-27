using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Configuration;

namespace WakuLive
{
    public class WakuLiveConfiguration : IWakuLiveConfiguration
    {
        public IAccountConfiguration Account { get; private set; } = new AccountConfiguration();

        private WakuLiveConfiguration() { }

        private WakuLiveConfiguration(SerializableWakuLiveConfiguration source) 
        {
            Account = new AccountConfiguration(source.Account);
        }

        public static WakuLiveConfiguration LoadOrDefault() 
        {
            var source = ConfigurationProvider.Load();
            var configuration = source == null ? new WakuLiveConfiguration() : new WakuLiveConfiguration(source);
            return configuration;
        }

        public static void Save(WakuLiveConfiguration config) 
        {
            var serializableConfig = new SerializableWakuLiveConfiguration();
            serializableConfig.Account = ((AccountConfiguration)config.Account).ToSerializable();

            ConfigurationProvider.Save(serializableConfig);
        }
    }
}
