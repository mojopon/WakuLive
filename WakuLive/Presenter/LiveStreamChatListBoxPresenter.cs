using Livet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Core.Domain;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamChatListBoxPresenter : IDisposable
    {
        private IDisposable _disposable;
        private LiveStreamChatListBoxViewModel _viewModel;

        public LiveStreamChatListBoxPresenter() { }

        public void SetViewModel(LiveStreamChatListBoxViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        public void SetChatModel(ChatModel model)
        {
            _disposable?.Dispose();
            _disposable = model.ChatMessageObservable
                               .Subscribe(x => _viewModel.AddChatMessage(x));    
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _viewModel?.Dispose();
        }
    }
}
