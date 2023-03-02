using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.Stub
{
    public class InMemoryTwitchChatDataStore : ITwitchChatDataStore
    {
        public TwitchChatClientEntity GetTwitchChatClient(string id, string userName, string channelName, string accessToken)
        {
            var compositeDisposable = new CompositeDisposable();
            var _subject = new Subject<TwitchChatMessageEntity>();

            int i = 0;
            var intervalDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
                                               .Subscribe(x =>
                                               {
                                                   i++;
                                                   var message = CreateMessage(i.ToString());
                                                   _subject.OnNext(message);
                                               });

            var completeOnDispose = Disposable.Create(() =>
            {
                _subject.OnCompleted();
            });

            compositeDisposable.Add(completeOnDispose);
            compositeDisposable.Add(intervalDisposable);
            compositeDisposable.Add(_subject);

            var entity = new TwitchChatClientEntity(id, _subject, compositeDisposable);
            return entity;
        }

        private TwitchChatMessageEntity CreateMessage(string message) 
        {
            var data = new TwitchChatMessageEntityData()
            {
                DisplayName = "DisplayName",
                UserName = "UserName",
                Message = message,
            };

            return new TwitchChatMessageEntity(data);
        }
    }
}
