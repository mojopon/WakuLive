using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WakuLive.Controller;

namespace WakuLive.Command
{
    public class LinkTwitchAccountCommand : ICommand
    {
        private AccountConfigurationController _controller;

        public event EventHandler? CanExecuteChanged;

        public LinkTwitchAccountCommand(AccountConfigurationController controller) 
        {
            _controller = controller;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _controller.LinkTwitchAccount();
        }
    }
}
