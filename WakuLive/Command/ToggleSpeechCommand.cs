using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WakuLive.Controller;

namespace WakuLive.Command
{
    public class ToggleSpeechCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private string _chatId;
        private TextToSpeechController _controller;
        public ToggleSpeechCommand(string chatId, TextToSpeechController controller) 
        {
            _chatId = chatId;
            _controller = controller;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var flag = (bool)parameter;

            _controller.ToggleSpeech(_chatId, flag);
        }
    }
}
