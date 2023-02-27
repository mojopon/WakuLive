using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChatClientEntityTranslator
    {
        public ChatModel Translate(TwitchChatClientEntity entity) 
        {
            if (entity != null)
            {
                Subject<ChatMessageModel> subject = new Subject<ChatMessageModel>();
                entity.ChatMessageObservable
                      .Subscribe(x =>
                      {
                          var data = new ChatMessageModelData()
                          {
                              UserId = x.UserId,
                              UserName = x.UserName,
                              DisplayName = x.DisplayName,
                              Message = x.Message,
                              UserType = x.UserType,
                          };
                          var messageModel = new ChatMessageModel(data);
                          subject.OnNext(messageModel);
                      },
                      ex => subject.OnError(ex),
                      () => subject.OnCompleted());

                var model = new ChatModel(entity.Id, subject);
                return model;
            }
            else 
            {
                return null;
            }
        }
    }
}
