using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Core.Domain;
using WakuLive.Interface;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamChannelTabPresenter 
    {
        private LiveStreamChannelTabViewModel _channelTabViewModel;
        private LiveStreamChannelTabItemsPresenter _channelTabItemsPresenter;

        public LiveStreamChannelTabPresenter(LiveStreamChannelTabViewModel tabViewModel, LiveStreamChannelTabItemsPresenter tabItemsPresenter) 
        {
            _channelTabViewModel = tabViewModel;
            _channelTabItemsPresenter = tabItemsPresenter;
        }

        /// <summary>
        /// ChannelTabItemsPresenterにModelをセットする
        /// </summary>
        /// <param name="chatModel"></param>
        /// <param name="commandFactory"></param>
        public void SetModels(string id, ChatModel chatModel, ChannelModel channelModel, ICommandFactory commandFactory)
        {
            _channelTabItemsPresenter.SetModels(_channelTabViewModel, id, chatModel, channelModel, commandFactory);
        }

        /// <summary>
        /// ChannelTabItemsPresenterにセットされたModelを削除する
        /// </summary>
        public void DeleteModels(string id)
        {
            Debug.Print("Remove Chat Model:" + id);
            _channelTabViewModel.RemoveTabItem(id);
            _channelTabItemsPresenter.DeleteModels(id);
        }
    }
}
