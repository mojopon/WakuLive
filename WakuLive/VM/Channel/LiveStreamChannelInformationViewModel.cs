using Livet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.VM
{
    public class LiveStreamChannelInformationViewModel : ViewModel
    {
        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> BroadcasterName { get; private set; } = new ReactiveProperty<string>();
        public ReactiveProperty<int> ViewerCount { get; private set; } = new ReactiveProperty<int>();

        public void SetChannelInformation(ChannelInformationModel model) 
        {
            Title.Value = model.Title;
            BroadcasterName.Value = model.BroadcasterName;
            ViewerCount.Value = model.ViewerCount;
        }
    }
}
