using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.VM
{
    public class ConfigurationWindowViewModel
    {
        public ConfigurationTabViewModel ConfigurationTabViewModel { get; set; }
        public ConfigurationWindowViewModel(ConfigurationTabViewModel configurationTabViewModel)
        {
            ConfigurationTabViewModel = configurationTabViewModel;
        }
    }
}
