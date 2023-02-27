using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Interface
{
    public interface IWakuLiveClient
    {
        void OpenMainWindow();
        void OpenConfigurationWindow();
        void ExitApplication();
        void AddTwitchAccessToken(string accessToken);
        void RemoveTwitchAccessToken();
    }
}
