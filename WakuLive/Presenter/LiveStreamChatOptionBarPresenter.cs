using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamChatOptionBarPresenter
    {
        private LiveStreamChatOptionBarViewModel _viewModel;
        public LiveStreamChatOptionBarPresenter(LiveStreamChatOptionBarViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        public void SetChatId(string id) 
        {
            _viewModel.SetChatId(id);
        }
    }
}
