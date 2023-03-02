using Microsoft.Extensions.DependencyInjection;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Controller;
using WakuLive.Interface;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamAddressBarPresenter : ICommandBindable
    {
        private CompositeDisposable disposables = new CompositeDisposable();

        private LiveStreamAddressBarViewModel _viewModel;
        public LiveStreamAddressBarPresenter(LiveStreamAddressBarViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        public void Bind(ServiceProvider provider)
        {
            var commandFactory = provider.GetRequiredService<ICommandFactory>();
            var connectChatCommand = commandFactory.GetConnectChatCommand();

            _viewModel.ConnectChatCommand = _viewModel.AddressText
                                                      .Select(x => !string.IsNullOrEmpty(x))
                                                      .ToReactiveCommand()
                                                      .WithSubscribe(() => connectChatCommand.Execute(_viewModel.AddressText.Value))
                                                      .AddTo(disposables);
        }
    }
}
