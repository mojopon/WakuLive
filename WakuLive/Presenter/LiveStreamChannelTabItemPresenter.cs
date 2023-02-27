using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Core.Domain;
using WakuLive.Interface;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamChannelTabItemPresenter
    {
        private LiveStreamChannelTabItemViewModel _tabItemViewModel;

        private LiveStreamChatListBoxPresenter _chatListBoxPresenter;
        private LiveStreamChannelInformationPresenter _channelInformationPresenter;

        public LiveStreamChannelTabItemPresenter(LiveStreamChannelTabItemViewModel viewModel) 
        {
            _tabItemViewModel = viewModel;
        }

        public void AddChatModel(ChatModel model, ICommandFactory commandFactory) 
        {
            _tabItemViewModel.AddChatModel(model);
            _tabItemViewModel.AddDisconnectChatCommand(commandFactory.GetDisconnectChatCommand());
            var listBoxViewModel = _tabItemViewModel.CreateChatListBox();
            _chatListBoxPresenter = new LiveStreamChatListBoxPresenter(listBoxViewModel);
            _chatListBoxPresenter.AddChatModel(model);
        }

        public void AddChannelModel(ChannelModel model, ICommandFactory commandFactory)
        {
            var channelInformationViewModel = _tabItemViewModel.CreateChannelInformation();
            _channelInformationPresenter = new LiveStreamChannelInformationPresenter(channelInformationViewModel);
            _channelInformationPresenter.AddChannelModel(model);
        }

        public void DeleteModels()
        {
            _chatListBoxPresenter.DeleteChatModel();
            _channelInformationPresenter.DeleteChannelModel();
            _tabItemViewModel.Dispose();
            _chatListBoxPresenter = null;
            _channelInformationPresenter = null;
        }
    }
}
