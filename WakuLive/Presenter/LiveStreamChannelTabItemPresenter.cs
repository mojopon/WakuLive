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
    public class LiveStreamChannelTabItemPresenter : IDisposable
    {
        private LiveStreamChannelTabItemViewModel _viewModel;
        public LiveStreamChannelTabItemPresenter(LiveStreamChannelTabItemViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        public void SetChatModel(string id, ChatModel model, ICommandFactory commandFactory) 
        {
            _viewModel.SetId(id);
            _viewModel.SetDisconnectChatCommand(commandFactory.GetDisconnectChatCommand());
        }

        public void Dispose()
        {
            _viewModel.Dispose();
        }
    }
}
