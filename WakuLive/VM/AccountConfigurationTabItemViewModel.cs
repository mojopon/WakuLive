using Livet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.VM
{
    public class AccountConfigurationTabItemViewModel : ViewModel
    {
        private ReactiveCommand _linkTwitchCommand;
        public ReactiveCommand LinkTwitchCommand 
        {
            get { return _linkTwitchCommand; }
            set 
            {
                _linkTwitchCommand = value;
                RaisePropertyChanged();
            }
        }

        private ReactiveCommand _unlinkTwitchCommand;
        public ReactiveCommand UnlinkTwitchCommand
        {
            get { return _unlinkTwitchCommand; }
            set
            {
                _unlinkTwitchCommand = value;
                RaisePropertyChanged();
            }
        }

        public ReactiveProperty<string> LinkTwitchButtonText { get; set; } = new ReactiveProperty<string>();

        public AccountConfigurationTabItemViewModel() 
        {
            SetTwitchButtonLinked(false);
        }

        public void SetTwitchButtonLinked(bool flag) 
        {
            LinkTwitchButtonText.Value = flag ? "認証済み" : "認証";
        }
    }
}
