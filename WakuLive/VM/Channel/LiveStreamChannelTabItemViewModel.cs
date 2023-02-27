using Livet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WakuLive.Command;
using WakuLive.Core.Domain;

namespace WakuLive.VM
{
    public class LiveStreamChannelTabItemViewModel : ViewModel
    {
        public string Id { get; private set; }
        public ReactiveProperty<string> Name { get; private set; } = new ReactiveProperty<string>();
        public LiveStreamChatListBoxViewModel LiveStreamChatListBoxViewModel { get; private set; }
        public LiveStreamChannelInformationViewModel LiveStreamChannelInformationViewModel { get; private set; }
        public ReactiveCommand CloseCommand { get; private set; }

        public LiveStreamChannelTabItemViewModel(){ }

        public void AddChatModel(ChatModel model) 
        {
            Id = model.Id;
            Name = new ReactiveProperty<string>(model.Id);
        }

        public void AddDisconnectChatCommand(DisconnectChatCommand disconnectChatCommand) 
        {
            var command = new ReactiveCommand(Observable.Return(true)).WithSubscribe(() => 
            {
                disconnectChatCommand.Execute(Id);
                this.Dispose();
            });

            CloseCommand = command;
        }

        public LiveStreamChatListBoxViewModel CreateChatListBox() 
        {
            var viewModel = new LiveStreamChatListBoxViewModel();
            LiveStreamChatListBoxViewModel = viewModel;
            return viewModel;
        }

        public LiveStreamChannelInformationViewModel CreateChannelInformation() 
        {
            var viewModel = new LiveStreamChannelInformationViewModel();
            LiveStreamChannelInformationViewModel = viewModel;
            return viewModel;
        }
    }
}
