using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Core.Domain;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamChannelInformationsPresenter
    {
        public LiveStreamChannelInformationsPresenter() { }

        private Dictionary<string, LiveStreamChannelInformationPresenter> channelInformationPresentersDict = new Dictionary<string, LiveStreamChannelInformationPresenter>();
        public void SetChannelModel(LiveStreamChannelTabItemViewModel parent, string id, ChannelModel channelModel, ICommandFactory commandFactory)
        {
            var channelInformation = new LiveStreamChannelInformationViewModel();
            parent.SetChannelInformation(channelInformation);

            var channelInformationPresenter = new LiveStreamChannelInformationPresenter();
            channelInformationPresenter.SetViewModel(channelInformation);
            channelInformationPresentersDict.Add(id, channelInformationPresenter);
            channelInformationPresenter.AddChannelModel(channelModel);
        }

        public void DeleteChannelModel(string id) 
        {
            channelInformationPresentersDict[id].Dispose();
            channelInformationPresentersDict.Remove(id);
        }
    }
}
