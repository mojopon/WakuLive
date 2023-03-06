using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.VM
{
    public class StatusBarViewModel : ViewModel
    {
        private string _statusText;
        public string StatusText 
        {
            get { return _statusText; }
            private set 
            {
                _statusText = value;
                RaisePropertyChanged();
            }
        }
        public StatusBarViewModel() { }

        public void SetStatusText(string text) 
        {
            StatusText = text;
        }
    }
}
