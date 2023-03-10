using Livet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WakuLive.Core.Domain;
using System.Windows;

namespace WakuLive.VM
{
    public class LiveStreamChannelInformationViewModel : ViewModel
    {
        private string _thumbnailUrl;
        public ReactiveProperty<BitmapImage> ThumbnailImage { get; private set; } = new ReactiveProperty<BitmapImage>();
        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> BroadcasterName { get; private set; } = new ReactiveProperty<string>();
        public ReactiveProperty<int> ViewerCount { get; private set; } = new ReactiveProperty<int>();

        public void SetChannelInformation(ChannelInformationModel model) 
        {
            Title.Value = model.Title;
            BroadcasterName.Value = model.BroadcasterName;
            ViewerCount.Value = model.ViewerCount;

            if (_thumbnailUrl != model.ThumbnailUrl) 
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _thumbnailUrl = model.ThumbnailUrl;
                    var imageUri = new Uri(_thumbnailUrl);
                    ThumbnailImage.Value = new BitmapImage(imageUri);
                });
            }
        }
    }
}
