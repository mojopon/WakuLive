using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Presenter
{
    public interface ILiveStreamPresenter
    {
        void AddModels(string id, ChatModel chatModel, ChannelModel channelModel);
        void DeleteModels(string id);
    }
}
