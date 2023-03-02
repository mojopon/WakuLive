using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Test
{
    public class TwitchChatDataStoreForTest : ITwitchChatDataStore
    {
        private Dictionary<string, Subject<TwitchChatMessageEntity>> subjectDict = new Dictionary<string, Subject<TwitchChatMessageEntity>>();

        public TwitchChatClientEntity GetTwitchChatClient(string id, string userName, string channelName, string accessToken)
        {
            var subject = new Subject<TwitchChatMessageEntity>();
            subjectDict.Add(id, subject);
            var disposable = Disposable.Create(() => 
            {
                subject.Dispose();
                subjectDict.Remove(id);
            });
            return new TwitchChatClientEntity(id, subject, disposable);
        }

        public void SendChatMessage(string id, TwitchChatMessageEntityData data) 
        {
            var entity = new TwitchChatMessageEntity(data);
            subjectDict[id].OnNext(entity);
        }
    }
}
