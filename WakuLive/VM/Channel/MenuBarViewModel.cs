using Livet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;

namespace WakuLive.VM
{
    public class MenuBarViewModel : ViewModel
    {
        private OpenConfigurationWindowCommand _openConfigurationCommand;
        public OpenConfigurationWindowCommand OpenConfigurationCommand
        {
            get { return _openConfigurationCommand; }
            set
            {
                _openConfigurationCommand = value;
                RaisePropertyChanged();
            }
        }

        public MenuBarViewModel() 
        {

        }
    }
}
