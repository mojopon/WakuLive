using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain.Twitch.Chat.Model;

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
                              Emotes = ChatEmoteEntityToChatEmoteModel(x.Emotes),
                              Message = x.Message,
                              UserType = x.UserType,
                              Color = x.Color,
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

        private List<ChatEmoteModel> ChatEmoteEntityToChatEmoteModel(List<TwitchChatEmoteEntity> source) 
        {
            var emotes = new List<ChatEmoteModel>();

            foreach (var entity in source) 
            {
                emotes.Add(new ChatEmoteModel(entity.Id, entity.StartIndex, entity.EndIndex, entity.Name, entity.ImageUrl));
            }

            return emotes;
        }
    }
}
