using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ChatModel
    {
        public string Id { get; private set; }
        public IObservable<ChatMessageModel> ChatMessageObservable { get; private set; }

        public ChatModel(string id,IObservable<ChatMessageModel> observable)
        {
            Id = id;
            ChatMessageObservable = observable;
        }
    }
}
