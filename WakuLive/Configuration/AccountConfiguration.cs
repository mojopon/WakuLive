using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Configuration
{
    public class AccountConfiguration : IAccountConfiguration
    {
        public ReactiveProperty<string> TwitchAccessToken { get; private set; } = new ReactiveProperty<string>();

        public AccountConfiguration() { }

        public AccountConfiguration(SerializableAccountConfiguration source) 
        {
            TwitchAccessToken = new ReactiveProperty<string>(source.TwitchAccessToken);
        }

        public SerializableAccountConfiguration ToSerializable() 
        {
            var config = new SerializableAccountConfiguration();
            config.TwitchAccessToken = TwitchAccessToken.Value;
            return config;
        }
    }
}
