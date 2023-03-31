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
using WakuLive.View;

namespace WakuLive.VM
{
    public class LiveStreamChannelTabItemViewModel : ViewModel
    {
        public string Id { get; private set; }
        public ReactiveProperty<string> Name { get; private set; } = new ReactiveProperty<string>();
        public LiveStreamChatListBoxViewModel LiveStreamChatListBoxViewModel { get; private set; }
        public LiveStreamChatOptionBarViewModel LiveStreamChatOptionBarViewModel { get; private set; }
        public LiveStreamChannelInformationViewModel LiveStreamChannelInformationViewModel { get; private set; }
        public ReactiveCommand CloseCommand { get; private set; }

        public LiveStreamChannelTabItemViewModel() { }

        public void SetId(string id)
        {
            Id = id;
            Name = new ReactiveProperty<string>(id);
        }

        public void SetDisconnectChatCommand(DisconnectChatCommand disconnectChatCommand)
        {
            var command = new ReactiveCommand(Observable.Return(true)).WithSubscribe(() =>
            {
                disconnectChatCommand.Execute(Id);
                this.Dispose();
            });

            CloseCommand = command;
        }

        public void SetChatListBox(LiveStreamChatListBoxViewModel listBox)
        {
            LiveStreamChatListBoxViewModel = listBox;
        }

        public void SetChatOptionBar(LiveStreamChatOptionBarViewModel optionBar) 
        {
            LiveStreamChatOptionBarViewModel = optionBar;
        }

        public void SetChannelInformation(LiveStreamChannelInformationViewModel channelInformation)
        {
            LiveStreamChannelInformationViewModel = channelInformation;
        }
    }
}
