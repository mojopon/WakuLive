using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamChatOptionBarsPresenter
    {
        private Dictionary<string, LiveStreamChatOptionBarPresenter> chatOptionBarPresenterDict = new Dictionary<string, LiveStreamChatOptionBarPresenter>();

        public void SetChatId(LiveStreamChannelTabItemViewModel parent, string id, ICommandFactory commandFactory) 
        {
            var chatOptionBar = new LiveStreamChatOptionBarViewModel();
            parent.SetChatOptionBar(chatOptionBar);

            var chatOptionBarPresenter = new LiveStreamChatOptionBarPresenter(chatOptionBar);
            chatOptionBarPresenterDict.Add(id, chatOptionBarPresenter);
            chatOptionBarPresenter.SetChatId(id);
            chatOptionBarPresenter.SetToggleSpeechCommand(commandFactory.GetToggleSpeechCommand(id));
        }
    }
}
