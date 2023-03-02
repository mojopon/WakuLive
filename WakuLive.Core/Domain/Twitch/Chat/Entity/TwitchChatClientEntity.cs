using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChatClientEntity : IDisposable
    {
        private IDisposable _disposable;

        public string Id { get; private set; }
        public IObservable<TwitchChatMessageEntity> ChatMessageObservable { get; private set; }

        public bool IsDisposed { get; private set; } = false;

        public TwitchChatClientEntity(string id, IObservable<TwitchChatMessageEntity> observable, IDisposable disposable) 
        {
            Id = id;
            ChatMessageObservable = observable;
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
            IsDisposed = true;
        }
    }
}
