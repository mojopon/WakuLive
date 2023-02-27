using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChatClientEntity
    {
        public string Id { get; private set; }
        public IObservable<TwitchChatMessageEntity> ChatMessageObservable { get; private set; }

        public TwitchChatClientEntity(string id, IObservable<TwitchChatMessageEntity> observable) 
        {
            Id = id;
            ChatMessageObservable = observable;
        }
    }
}
