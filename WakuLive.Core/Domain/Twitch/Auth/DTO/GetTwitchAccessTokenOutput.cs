using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class GetTwitchAccessTokenOutput
    {
        public IObservable<string> AccessTokenObservable { get; private set; }

        public GetTwitchAccessTokenOutput(IObservable<string> accessTokenObservable)
        {
            AccessTokenObservable = accessTokenObservable;
        }
    }
}
