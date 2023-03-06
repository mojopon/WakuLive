using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;
using WakuLive.Presenter;

namespace WakuLive.Controller
{
    public class StatusBarController
    {
        private StatusBarPresenter _statusBarPresenter;
        public StatusBarController(StatusBarPresenter statusBarPresenter) 
        {
            _statusBarPresenter = statusBarPresenter;
        }

        public void AddChannelModel(string id, ChannelModel channelModel) 
        {
            _statusBarPresenter.AddChannelModel(id, channelModel);
        }

        public void DeleteChannelModel(string id)
        {
            _statusBarPresenter.DeleteChannelModel(id);
        }
    }
}
