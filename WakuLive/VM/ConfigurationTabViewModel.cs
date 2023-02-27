using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.VM
{
    public class ConfigurationTabViewModel
    {
        public AccountConfigurationTabItemViewModel AccountConfigurationTabItemViewModel { get; set; }
        public ConfigurationTabViewModel(AccountConfigurationTabItemViewModel accountConfigurationTabItemViewModel) 
        {
            AccountConfigurationTabItemViewModel = accountConfigurationTabItemViewModel;
        }
    }
}
