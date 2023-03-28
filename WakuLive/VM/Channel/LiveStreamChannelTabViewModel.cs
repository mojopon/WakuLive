using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Reactive.Bindings;
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;
using WakuLive.Core.Domain;
using WakuLive.Interface;

namespace WakuLive.VM
{
    public class LiveStreamChannelTabViewModel : ViewModel
    {
        public Action ShowLatestTab { get; set; }

        public ReactiveCollection<LiveStreamChannelTabItemViewModel> Items { get; set; } = new ReactiveCollection<LiveStreamChannelTabItemViewModel>();

        public LiveStreamChannelTabViewModel() { }

        public void AddTabItem(LiveStreamChannelTabItemViewModel tabItem) 
        {
            Items.Add(tabItem);
            ShowLatestTab();
        }

        public void RemoveTabItem(string id) 
        {
            var item = Items.Where(x => x.Id == id)
                            .First();
            if (item != null)
            {
                Items.Remove(item);
            }
        }
    }
}
