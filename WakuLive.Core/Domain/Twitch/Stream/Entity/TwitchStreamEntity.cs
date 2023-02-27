using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchStreamEntity
    {
        public string Id { get; private set; }
        public IObservable<TwitchStreamInformationEntity> StreamInformationObservable { get; private set; }

        public TwitchStreamEntity(string id, IObservable<TwitchStreamInformationEntity> streamInformationObservable) 
        {
            Id = id;
            StreamInformationObservable = streamInformationObservable;
        }
    }
}
