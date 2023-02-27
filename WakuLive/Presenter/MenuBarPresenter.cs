using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Interface;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class MenuBarPresenter : ICommandBindable
    {
        private MenuBarViewModel _viewModel;
        public MenuBarPresenter(MenuBarViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Bind(ServiceProvider provider)
        {
            var commandFactory = provider.GetRequiredService<ICommandFactory>();
            _viewModel.OpenConfigurationCommand = commandFactory.GetOpenConfigurationWindowCommand();
        }
    }
}
