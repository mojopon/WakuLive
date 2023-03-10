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
    public class LiveStreamChannelTabItemsPresenter
    {
        private LiveStreamChatListBoxesPresenter _chatListBoxesPresenter;
        private LiveStreamChannelInformationsPresenter _channelInformationsPresenter;
        public LiveStreamChannelTabItemsPresenter(LiveStreamChatListBoxesPresenter chatListBoxesPresenter,
                                                  LiveStreamChannelInformationsPresenter channelInformationsPresenter) 
        {
            _chatListBoxesPresenter = chatListBoxesPresenter;
            _channelInformationsPresenter = channelInformationsPresenter;
        }

        private Dictionary<string, LiveStreamChannelTabItemPresenter> tabItemPresenterDic = new Dictionary<string, LiveStreamChannelTabItemPresenter>();
        public void SetModels(LiveStreamChannelTabViewModel parent, string id, ChatModel chatModel, ChannelModel channelModel, ICommandFactory commandFactory)
        {
            var tabItem = new LiveStreamChannelTabItemViewModel();
            parent.AddTabItem(tabItem);

            var tabItemPresenter = new LiveStreamChannelTabItemPresenter(tabItem);
            tabItemPresenterDic.Add(id, tabItemPresenter);
            tabItemPresenter.SetChatModel(id, chatModel, commandFactory);

            _chatListBoxesPresenter.SetChatModel(tabItem, id, chatModel, commandFactory);
            _channelInformationsPresenter.SetChannelModel(tabItem, id, channelModel, commandFactory);
        }

        public void DeleteModels(string id) 
        {
            _chatListBoxesPresenter.DeleteChatModel(id);
            _channelInformationsPresenter.DeleteChannelModel(id);

            tabItemPresenterDic[id].Dispose();
            tabItemPresenterDic.Remove(id);
        }
    }
}
