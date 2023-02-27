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
        public ReactiveCollection<LiveStreamChannelTabItemViewModel> Items { get; set; } = new ReactiveCollection<LiveStreamChannelTabItemViewModel>();

        public LiveStreamChannelTabViewModel() { }

        public LiveStreamChannelTabItemViewModel CreateItem()
        {
            var viewModel = new LiveStreamChannelTabItemViewModel();
            Items.Add(viewModel);
            return viewModel;
        }

        public void RemoveItem(string id) 
        {
            var content = Items.Where(x => x.Id == id)
                                  .First();
            if (content != null)
            {
                Items.Remove(content);
            }
        }
    }
}
