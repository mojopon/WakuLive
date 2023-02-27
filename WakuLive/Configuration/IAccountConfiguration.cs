using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Configuration
{
    public interface IAccountConfiguration
    {
        ReactiveProperty<string> TwitchAccessToken { get; }
    }
}
