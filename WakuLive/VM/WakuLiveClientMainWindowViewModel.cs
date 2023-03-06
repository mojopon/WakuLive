using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Core.Domain;
using WakuLive.Interface;

namespace WakuLive.VM
{
    public class WakuLiveClientMainWindowViewModel
    {
        public StatusBarViewModel StatusBarViewModel { get; set; }
        public MenuBarViewModel MenuBarViewModel { get; set; }
        public LiveStreamAddressBarViewModel LiveStreamAddressBarViewModel { get; set; }
        public LiveStreamChannelTabViewModel LiveStreamChatTabViewModel { get; set; }

        public WakuLiveClientMainWindowViewModel(StatusBarViewModel statusBarViewModel,
                                                 MenuBarViewModel menuBarViewModel,
                                                 LiveStreamAddressBarViewModel liveStreamAddressBarViewModel,
                                                 LiveStreamChannelTabViewModel liveStreamChatTabViewModel) 
        {
            StatusBarViewModel = statusBarViewModel;
            MenuBarViewModel = menuBarViewModel;
            LiveStreamAddressBarViewModel = liveStreamAddressBarViewModel;
            LiveStreamChatTabViewModel = liveStreamChatTabViewModel;
        }
    }
}
