using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Core.Domain;
using WakuLive.Interface;

namespace WakuLive.Presenter
{
    public class LiveStreamPresenter : ILiveStreamPresenter, ICommandBindable
    {
        private ICommandFactory _commandFactory;

        private LiveStreamChannelTabPresenter _liveStreamChannelTabPresenter;
        public LiveStreamPresenter(LiveStreamChannelTabPresenter liveStreamChannelTabPresenter) 
        {
            _liveStreamChannelTabPresenter = liveStreamChannelTabPresenter;
        }

        public void AddModels(string id, ChatModel chatModel, ChannelModel channelModel)
        {
            _liveStreamChannelTabPresenter.AddModels(id, chatModel, channelModel, _commandFactory);
        }

        public void DeleteModels(string id)
        {
            _liveStreamChannelTabPresenter.DeleteModels(id);
        }

        public void Bind(ServiceProvider provider)
        {
            _commandFactory = provider.GetRequiredService<ICommandFactory>();
        }
    }
}
