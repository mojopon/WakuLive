using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChannelEntity : IDisposable
    {
        private IDisposable _disposable;

        public string Id { get; private set; }
        public IObservable<TwitchChannelInformationEntity> ChannelInformationObservable { get; private set; }

        public bool IsDisposed { get; private set; } = false;

        public TwitchChannelEntity(string id, IObservable<TwitchChannelInformationEntity> channelInformationObservable, IDisposable disposable) 
        {
            Id = id;
            ChannelInformationObservable = channelInformationObservable;
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            IsDisposed = true;
        }
    }
}
