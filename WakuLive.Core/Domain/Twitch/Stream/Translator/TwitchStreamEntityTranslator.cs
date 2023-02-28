using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchStreamEntityTranslator
    {
        public ChannelModel Translate(TwitchStreamEntity entity) 
        {
            if (entity != null)
            {
                Subject<ChannelInformationModel> subject = new Subject<ChannelInformationModel>();
                entity.StreamInformationObservable
                      .Subscribe(x =>
                                {
                                    var data = new ChannelInformationModelData()
                                    {
                                        ViewerCount = x.ViewerCount,
                                        Title = x.Title,
                                        BroadcasterName = x.BroadcasterName,
                                        IsStreaming = x.IsStreaming,
                                    };
                                    var channelInformationModel = new ChannelInformationModel(data);
                                    subject.OnNext(channelInformationModel);
                                },
                                ex => subject.OnError(ex),
                                () => subject.OnCompleted());

                var model = new ChannelModel(entity.Id, subject);
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
