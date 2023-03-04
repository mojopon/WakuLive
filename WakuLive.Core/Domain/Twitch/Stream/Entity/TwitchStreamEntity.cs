using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchStreamEntity : IDisposable
    {
        private IDisposable _disposable;

        public string Id { get; private set; }
        public IObservable<TwitchStreamInformationEntity> StreamInformationObservable { get; private set; }

        public bool IsDisposed { get; private set; } = false;

        public TwitchStreamEntity(string id, IObservable<TwitchStreamInformationEntity> streamInformationObservable, IDisposable disposable) 
        {
            Id = id;
            StreamInformationObservable = streamInformationObservable;
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            IsDisposed = true;
        }
    }
}
