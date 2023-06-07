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
    public class LiveStreamChatListBoxesPresenter
    {
        public LiveStreamChatListBoxesPresenter() { }

        private Dictionary<string, LiveStreamChatListBoxPresenter> chatListBoxPresenterDict = new Dictionary<string, LiveStreamChatListBoxPresenter>();
        public void SetChatModel(LiveStreamChannelTabItemViewModel parent, string id, ChatModel chatModel, ICommandFactory commandFactory)
        {
            var chatListBox = new LiveStreamChatListBoxViewModel();
            parent.SetChatListBox(chatListBox);

            var chatListBoxPresenter = new LiveStreamChatListBoxPresenter(chatListBox);
            chatListBoxPresenterDict.Add(id, chatListBoxPresenter);
            chatListBoxPresenter.SetChatModel(chatModel);
        }

        public void DeleteChatModel(string id) 
        {
            chatListBoxPresenterDict[id].Dispose();
            chatListBoxPresenterDict.Remove(id);
        }

        public void ToggleAutoScroll(string id, bool flag) 
        {
            chatListBoxPresenterDict[id].ToggleAutoScroll(flag);
        }
    }
}
