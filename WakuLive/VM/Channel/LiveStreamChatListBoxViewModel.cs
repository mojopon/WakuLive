using Livet;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WakuLive.Core.Domain;

namespace WakuLive.VM
{
    public class LiveStreamChatListBoxViewModel : ViewModel
    {
        public Action ScrollToBottom { get; set; }

        public ObservableCollection<LiveStreamChatListBoxItemViewModel> Items { get; set; } = new ObservableCollection<LiveStreamChatListBoxItemViewModel>();

        public LiveStreamChatListBoxViewModel()
        {
            BindingOperations.EnableCollectionSynchronization(Items, new object());
        }

        private int itemslimit = 1000;
        public void AddChatMessage(ChatMessageModel chatMessageModel)
        {
            var item = new LiveStreamChatListBoxItemViewModel
            {
                Name = chatMessageModel.UserName,
                Color = chatMessageModel.Color,
                Comment = chatMessageModel.Message,
                Emotes = chatMessageModel.Emotes,
            };

            Items.Add(item);

            // アイテム数が最大件数を超えたら、古いものから削除
            if (Items.Count > itemslimit) 
            {
                Items.RemoveAt(0);
            }

            ScrollToBottom?.Invoke();
        }
    }
}
