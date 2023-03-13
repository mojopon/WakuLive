using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.VM
{
    public class ConfigurationTabViewModel
    {
        public TwitchConfigurationTabItemViewModel TwitchConfigurationTabItemViewModel { get; set; }
        public ConfigurationTabViewModel(TwitchConfigurationTabItemViewModel twitchConfigurationTabItemViewModel) 
        {
            TwitchConfigurationTabItemViewModel = twitchConfigurationTabItemViewModel;
        }
    }
}
