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
        private LiveStreamChannelTabViewModel _tabViewModel;
        private Dictionary<string, LiveStreamChannelTabItemPresenter> _tabItemPresenters = new Dictionary<string, LiveStreamChannelTabItemPresenter>();

        public LiveStreamChannelTabPresenter(LiveStreamChannelTabViewModel viewModel) 
        {
            _tabViewModel = viewModel;
        }

        /// <summary>
        /// Modelsに対応したViewModel(View)とPresenterを作成して管理する
        /// </summary>
        /// <param name="chatModel"></param>
        /// <param name="commandFactory"></param>
        public void AddModels(string id, ChatModel chatModel, ChannelModel channelModel, ICommandFactory commandFactory)
        {
            var tabItemPresenter = GetOrCreateItem(id);
            tabItemPresenter.AddChatModel(chatModel, commandFactory);
            tabItemPresenter.AddChannelModel(channelModel, commandFactory);
        }

        private LiveStreamChannelTabItemPresenter GetOrCreateItem(string id) 
        {
            if (_tabItemPresenters.ContainsKey(id))
            {
                return _tabItemPresenters[id];
            }
            else
            {
                var tabItemViewModel = _tabViewModel.CreateItem();
                var tabItemPresenter = new LiveStreamChannelTabItemPresenter(tabItemViewModel);
                _tabItemPresenters.Add(id, tabItemPresenter);
                return tabItemPresenter;
            }
        }

        /// <summary>
        /// Idに対応したViewModel(View)とPresenterを削除・破棄する
        /// </summary>
        public void DeleteModels(string id)
        {
            if (_tabItemPresenters.ContainsKey(id)) 
            {
                Debug.Print("Remove Chat Model:" + id);
                _tabViewModel.RemoveItem(id);
                _tabItemPresenters[id].DeleteModels();
                _tabItemPresenters.Remove(id);
            }
        }
    }
}
