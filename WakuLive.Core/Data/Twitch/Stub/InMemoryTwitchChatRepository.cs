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
    public class InMemoryTwitchChatRepository : ITwitchChatRepository
    {
        private Dictionary<string, CompositeDisposable> _disposablesDic = new Dictionary<string, CompositeDisposable>();

        public TwitchChatClientEntity ConnectChat(string userName, string channelName, string accessToken)
        {
            var id = CreateId(channelName);
            if (!_disposablesDic.ContainsKey(id))
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

                _disposablesDic.Add(id, compositeDisposable);

                var entity = new TwitchChatClientEntity(id, _subject);

                Debug.Print("InMemoryTwitchChatRepository ConnectChat: ChannelName:" + channelName + ", AccessToken:" + accessToken);

                return entity;
            }
            else 
            {
                return null;
            }
        }

        private string CreateId(string channelName) 
        {
            return "InMemory-" + channelName;
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

        public void DisconnectChat(string id)
        {
            if (_disposablesDic.ContainsKey(id))
            {
                _disposablesDic[id].Dispose();
                _disposablesDic.Remove(id);
            }
        }
    }
}
