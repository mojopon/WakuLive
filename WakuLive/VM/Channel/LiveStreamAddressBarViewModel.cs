using Livet;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Controller;
using WakuLive.Interface;

namespace WakuLive.VM
{
    public class LiveStreamAddressBarViewModel : ViewModel
    {
        private ReactiveCommand _connectChatCommand;
        public ReactiveCommand ConnectChatCommand 
        {
            get { return _connectChatCommand; }
            set 
            { 
                _connectChatCommand = value;
                RaisePropertyChanged();
            }
        }

        public ReactiveProperty<string> AddressText { get; set; } = new ReactiveProperty<string>();

        public LiveStreamAddressBarViewModel() 
        {
            AddressText.Subscribe(x =>
            {
                Debug.Print("Address Text:" + x);
            });
        }
    }
}
